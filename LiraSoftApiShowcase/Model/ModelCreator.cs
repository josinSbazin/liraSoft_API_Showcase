using System.IO;
using ProjectIOHelper;
using PureAbstractions;

namespace LiraSoftApiShowcase.Model
{
    public class ModelCreator
    {
        public static void CreateEmptyModel(string fileName)
        {
            var ioHeader = ProjectIOHelperTypeInfo.get_TypeInfo().CreateIOHeader();
            ioHeader.Clear();
            var binaryPersistent = (IBinaryPersistent)ioHeader;
            var filePath = fileName;
            Stream output = File.Create(filePath);
            BinaryWriter pWriter = new BinaryWriter(output);
            output.Seek(0L, SeekOrigin.Begin);
            binaryPersistent.SaveState(pWriter, null);
            pWriter.Close();
            output.Close();
        }
    }
}