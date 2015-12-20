using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage.Streams;
using Windows.Web.Http;
using Windows.Web.Http.Headers;

namespace ProjectTDTUniversal.Models
{
    public class HttpFormContent : IHttpContent
    {
        HttpContentHeaderCollection _Headers;
        string _Content ;
        public HttpContentHeaderCollection Headers
        {
            get
            {
                return _Headers ?? new HttpContentHeaderCollection();
            }
            private set
            {
                _Headers = value ?? new HttpContentHeaderCollection()
                {
                    ContentType = new HttpMediaTypeHeaderValue("application/x-www-form-urlencoded") { CharSet = "UTF-8" }

                };
            }
        }

        public HttpFormContent(string[] format,params string[] args)
        {
            _Headers = new HttpContentHeaderCollection()
            {
                ContentType = new HttpMediaTypeHeaderValue("application/x-www-form-urlencoded") { CharSet = "UTF-8" }

            };
            _Headers.ContentEncoding.Add(HttpContentCodingHeaderValue.Parse("UTF-8"));
            StringBuilder sb = new StringBuilder();
            if (format.Length != args.Length || format.Length < 1)
            {
                throw new System.FormatException();
            }
            sb.Append(string.Format("{0}={1}", format[0], args[0]));
            for (int i = 1; i < format.Length; i++)
            {
                sb.Append("&");
                sb.Append(string.Format("{0}={1}", format[i], args[i]));               
            }
            _Content = sb.ToString();
        }


        public IAsyncOperationWithProgress<ulong, ulong> BufferAllAsync()
        {
            return AsyncInfo.Run<ulong, ulong>((cancellationToken, progress) =>
            {
                return Task<ulong>.Run(() =>
                {
                    ulong length = GetLength();

                    // Report progress.
                    progress.Report(length);

                    // Just return the size in bytes.
                    return length;
                });
            });
        }

        public void Dispose()
        {
            
        }

        public IAsyncOperationWithProgress<IBuffer, ulong> ReadAsBufferAsync()
        {
            return AsyncInfo.Run<IBuffer, ulong>((cancellationToken, progress) =>
            {
                return Task<IBuffer>.Run(() =>
                {
                    DataWriter writer = new DataWriter();
                    writer.WriteString(_Content);

                    // Make sure that the DataWriter destructor does not free the buffer.
                    IBuffer buffer = writer.DetachBuffer();

                    // Report progress.
                    progress.Report(buffer.Length);

                    return buffer;
                });
            });
        }

        public IAsyncOperationWithProgress<IInputStream, ulong> ReadAsInputStreamAsync()
        {
            return AsyncInfo.Run<IInputStream, ulong>(async (cancellationToken, progress) =>
            {
                InMemoryRandomAccessStream randomAccessStream = new InMemoryRandomAccessStream();
                DataWriter writer = new DataWriter(randomAccessStream);
                writer.WriteString(_Content);

                uint bytesStored = await writer.StoreAsync().AsTask(cancellationToken);

                // Make sure that the DataWriter destructor does not close the stream.
                writer.DetachStream();

                // Report progress.
                progress.Report(randomAccessStream.Size);

                return randomAccessStream.GetInputStreamAt(0);
            });
        }

        public IAsyncOperationWithProgress<string, ulong> ReadAsStringAsync()
        {
            return AsyncInfo.Run<string, ulong>((cancellationToken, progress) =>
            {
                return Task<string>.Run(() =>
                {
                 
                    // Report progress (length of string).
                    progress.Report((ulong)_Content.Length);

                    return _Content;
                });
            });
        }

        public bool TryComputeLength(out ulong length)
        {
            length = GetLength();
            return true;
        }

        public IAsyncOperationWithProgress<ulong, ulong> WriteToStreamAsync(IOutputStream outputStream)
        {
            return AsyncInfo.Run<ulong, ulong>(async (cancellationToken, progress) =>
            {
                DataWriter writer = new DataWriter(outputStream);
                writer.WriteString(_Content);
                uint bytesWritten = await writer.StoreAsync().AsTask(cancellationToken);

                // Make sure that DataWriter destructor does not close the stream.
                writer.DetachStream();

                // Report progress.
                progress.Report(bytesWritten);

                return bytesWritten;
            });
        }

        private ulong GetLength()
        {
            return (ulong)System.Text.Encoding.UTF8.GetByteCount(_Content);
        }
    }

}
