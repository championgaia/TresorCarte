using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public interface IGestionFichier_BLL
    {
        Task<List<string>> Lecture(string sFullPathFile);
        Task<string> Ecriture(string sFullPathFile);
    }
    public class GestionFichier_BLL : IGestionFichier_BLL
    {
        public async Task<List<string>> Lecture(string sFullPathFile)
        {
            List<string> oListDescriptionFichier = null;
            if (!string.IsNullOrEmpty(sFullPathFile))
            {
                FileInfo oFileInfo = new FileInfo(sFullPathFile);
                if (oFileInfo.Exists)
                {
                    oListDescriptionFichier = (await File.ReadAllLinesAsync(oFileInfo.FullName)).ToList();
                    if (oListDescriptionFichier != null && oListDescriptionFichier.Count > 0)
                    {
                        foreach (var oDescriptionFichier in oListDescriptionFichier)
                        {
                            if (oDescriptionFichier.StartsWith($"#"))
                            {
                                oListDescriptionFichier.Remove(oDescriptionFichier);
                            }
                        }
                    }
                    else
                    {

                    }
                }
                else
                {

                }
            }
            else
            {

            }
            return oListDescriptionFichier;
        }

        public async Task<string> Ecriture(string sFullPathFile)
        {
            throw new NotImplementedException();
        }
    }
}
