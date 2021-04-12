using BLL;
using Common;
using Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NoUnitTest
{
    public class GestionFichier_Test : NoUnitTestBase
    {
        private IGestionFichier_BLL _oIGestionFichier_BLL { get; set; }
        #region constant
        private const string REPERTOIRE_FICHIER = @"..\..\..\FichierTests";
        private const string FICHIER_OK= "tresor_carte.txt";
        private const string FICHIER_VIDE = "tresor_carte_vide.txt";
        private const string FICHIER_SANS_INFO_MAP = "tresor_carte_sans_info_carte.txt";
        private const string FICHIER_SANS_TRESOR = "tresor_carte_sans_tresor.txt";
        private const string FICHIER_SANS_JOUEUR = "tresor_carte_sans_joueur.txt";
        #endregion
        [SetUp]
        public void Setup()
        {
            _oIGestionFichier_BLL = new GestionFichier_BLL();
        }
        /// <summary>
        /// Test Lecture return null
        /// Task<List<string>> Lecture(string sFileName)
        /// </summary>
        [Test]
        public async Task Lecture_TestNull()
        {
            //Test sFileName = null
            List<string> oListeDonnes = await _oIGestionFichier_BLL.Lecture(null);
            Assert.IsNull(oListeDonnes);
            //Test sFileName est une chaine vide
            oListeDonnes = await _oIGestionFichier_BLL.Lecture(string.Empty);
            Assert.IsNull(oListeDonnes);
        }
        /// <summary>
        /// Test validation donnees
        /// Task<(bool, string, List<string>)> FichierDonnesValide(string sFileName)
        /// </summary>
        [Test]
        public async Task Lecture_TestValidationDonnes()
        {
            //Fichier vide
            (bool bEstValide, string sMessageErreur, List<string> oListeDescriptionFichier) = 
                await _oIGestionFichier_BLL.FichierDonnesValide(FICHIER_VIDE, Path.Combine(REPERTOIRE_FICHIER, FICHIER_VIDE));
            Assert.IsFalse(bEstValide);
            Assert.IsEmpty(oListeDescriptionFichier);
            Assert.IsTrue(sMessageErreur.Contains(Constants.DONNES_CARTE_INVALIDE) && sMessageErreur.Contains(Constants.DONNES_JOUEUR_INVALIDE) &&
                sMessageErreur.Contains(Constants.DONNES_TRESOR_INVALIDE));
            //fichier sans info carte
            (bEstValide, sMessageErreur, oListeDescriptionFichier) = await _oIGestionFichier_BLL.FichierDonnesValide(FICHIER_SANS_INFO_MAP, Path.Combine(REPERTOIRE_FICHIER, FICHIER_SANS_INFO_MAP));
            Assert.IsFalse(bEstValide);
            Assert.IsNotEmpty(oListeDescriptionFichier);
            Assert.IsTrue(sMessageErreur.Contains(Constants.DONNES_CARTE_INVALIDE));
            //fichier sans info trésor
            (bEstValide, sMessageErreur, oListeDescriptionFichier) = await _oIGestionFichier_BLL.FichierDonnesValide(FICHIER_SANS_TRESOR, Path.Combine(REPERTOIRE_FICHIER, FICHIER_SANS_TRESOR));
            Assert.IsFalse(bEstValide);
            Assert.IsNotEmpty(oListeDescriptionFichier);
            Assert.IsTrue(sMessageErreur.Contains(Constants.DONNES_TRESOR_INVALIDE));
            //fichier sans info joueur
            (bEstValide, sMessageErreur, oListeDescriptionFichier) = await _oIGestionFichier_BLL.FichierDonnesValide(FICHIER_SANS_JOUEUR, Path.Combine(REPERTOIRE_FICHIER, FICHIER_SANS_JOUEUR));
            Assert.IsFalse(bEstValide);
            Assert.IsNotEmpty(oListeDescriptionFichier);
            Assert.IsTrue(sMessageErreur.Contains(Constants.DONNES_JOUEUR_INVALIDE));
            //test ok
            (bEstValide, sMessageErreur, oListeDescriptionFichier) = await _oIGestionFichier_BLL.FichierDonnesValide(FICHIER_OK, Path.Combine(REPERTOIRE_FICHIER, FICHIER_OK));
            Assert.IsTrue(bEstValide);
            Assert.IsNotEmpty(oListeDescriptionFichier);
            Assert.IsTrue(!sMessageErreur.Contains(Constants.DONNES_CARTE_INVALIDE) && !sMessageErreur.Contains(Constants.DONNES_JOUEUR_INVALIDE) &&
                !sMessageErreur.Contains(Constants.DONNES_TRESOR_INVALIDE));
        }
    }
}
