using TestNinja.Mocking;

namespace TestNinja.Tests.Mocking;

public class FakeFileReader : IFileReader
{
    public string Read(string path)
    {
        return "";
    }
}

