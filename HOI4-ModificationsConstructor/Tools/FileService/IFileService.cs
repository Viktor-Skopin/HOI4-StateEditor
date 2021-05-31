using System.Collections.Generic;

namespace HOI4_ModificationsConstructor
{
    public interface IFileService
    {
        List<State> Open(string foldername);
        void Save(string foldername, List<State> states);
    }
}
