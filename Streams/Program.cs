using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace htmlStreams
{
    class Program
    {
        static void Main(string[] args)
        {
            htmldecr hd1 = new htmldecr(Directory.GetCurrentDirectory() + "\\test.xml");
            FileInfo fi = new FileInfo(Directory.GetCurrentDirectory() + "\\test.xml");
            byte[] btarr = new byte[fi.Length];

            hd1.Read(btarr, 0, (int)fi.Length);
            int u = hd1.Decode(btarr, 0, (int)fi.Length);           
            hd1.Write(btarr, 0, u);
            hd1.Close();
            Console.WriteLine("File 'test.xml' is successfully decoded. Check file 'out.xml' in the current path");
            Console.ReadKey();
        }       
    }

    class htmldecr : Stream
    {
        public FileStream fs1;
        public FileStream fs2;

        public void AddText(byte[] info1, int len)
        {
            fs2.Write(info1, 0, len);
        }

        public htmldecr(string path)
        {
            fs1 = File.OpenRead(path);
            fs2 = File.OpenWrite(Directory.GetCurrentDirectory() + "\\out.xml");
        }

        public override int Read(byte[] buffer, int offset, int count)
        {

            return fs1.Read(buffer, offset, count);
        }

        public int Decode(byte[] buffer, int offset, int count)
        {
            return HtmlDecoder.DecodeEntity(buffer, offset, count);
        }

        public override void Write(byte[] b, int i, int j)
        {
            fs2.Write(b, i, j);
        }

        public override void SetLength(long value)
        {
            fs1.SetLength(value);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return fs1.Seek(offset, origin);
        }

        public override long Position
        {
            get
            {
                return fs1.Position;
            }
            set
            {
                fs1.Position = value;
            }
        }

        public override long Length
        {
            get
            {
                return fs1.Length;
            }
        }

        public override void Flush()
        {
            fs1.Flush();
        }

        public override bool CanRead 
        {
            get
            {
                return fs1.CanRead;
            }
        }

        public override bool CanSeek
        {
            get
            {
                return fs1.CanSeek;
            }
        }

        public override bool CanWrite
        {
            get
            {
                return fs1.CanWrite;
            }
        }

    }
}
