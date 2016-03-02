using System;
using System.IO;
using Prospector.Domain.Contracts.Wrappers;

namespace Prospector.Infrastructure.Wrappers
{
    public class IoWrapper : IIoWrapper
    {
        public void Write(String filePath, String content)
        {
            File.WriteAllText(filePath, content);
        }

        public String Read(String filePath)
        {
            return File.ReadAllText(filePath);
        }
    }
}
