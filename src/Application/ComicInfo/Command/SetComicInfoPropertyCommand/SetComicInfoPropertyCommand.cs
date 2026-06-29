using Application.Interfaces;

namespace Application.ComicInfo.Command.SetComicInfoPropertyCommand
{
    public class SetComicInfoPropertyCommand : AuthenticatedRequest<bool>
    {
        public string FilePath { get; }
        public string PropertyName { get; }
        public string Value { get; }

        public SetComicInfoPropertyCommand(string filePath, string propertyName, string value)
        {
            FilePath = filePath;
            PropertyName = propertyName;
            Value = value;
        }
    }
}