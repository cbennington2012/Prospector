using System;

namespace Prospector.Domain.Contracts.Wrappers
{
    public interface IIoWrapper
    {
        void Write(String filePath, String content);
        String Read(String filePath);
    }
}
