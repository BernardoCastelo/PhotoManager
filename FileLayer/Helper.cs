using BusinessLayer.Models;
using System;
using System.IO;

namespace BusinessLayer
{
    public static class Helper
    {
        public static FileTypeEnum GetType(string filepath)
        {
            var extension = new FileInfo(filepath).Extension.Substring(1).ToUpper();
            if (!Enum.TryParse(typeof(FileTypeEnum), new FileInfo(filepath).Extension.Substring(1).ToUpper(), out object oType))
            {
                throw new InvalidTypeException(extension);
            }
            return (FileTypeEnum)oType;
        }
    }
}
