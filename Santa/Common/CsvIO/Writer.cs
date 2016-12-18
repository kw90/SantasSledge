using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Common.CsvIO
{
    public class Writer
    {
        public void Write(string path, string fineName, List<Gift> giftList)
        {
            using (var outputFile = new StreamWriter(path + fineName + ".csv"))
            {
                outputFile.WriteLine("GiftId,Latitude,Longitude,Weight");
                foreach (var gift in giftList)
                {
                    var builder = new StringBuilder();

                    builder.Append(gift.Id);
                    builder.Append(",");
                    builder.Append(gift.Location.Latitude);
                    builder.Append(",");
                    builder.Append(gift.Location.Longitude);
                    builder.Append(",");
                    builder.Append(gift.Weight);

                    outputFile.WriteLine(builder);
                }
            }
        }
    }
}
