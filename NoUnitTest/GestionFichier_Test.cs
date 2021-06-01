using BLL;
using Common;
using Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NoUnitTest
{
    public class GestionFichier_Test : NoUnitTestBase
    {
        private IFileManage_BLL _oIGestionFichier_BLL { get; set; }
        [SetUp]
        public void Setup()
        {
            _oIGestionFichier_BLL = new FileManage_BLL();
        }
        /// <summary
        /// Test Lecture return null
        /// Task<List<string>> Lecture(string sFileName)
        /// </summary>
        [Test]
        public async Task Lecture_TestNull()
        {
            //Test sFileName = null
            Assert.IsNull(await _oIGestionFichier_BLL.ReadAsync(null));
            //Test sFileName est une chaine vide
            Assert.IsNull(await _oIGestionFichier_BLL.ReadAsync(string.Empty));
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
                await _oIGestionFichier_BLL.DataFileValideAsync(FICHIER_VIDE, Path.Combine(REPERTOIRE_FICHIER, FICHIER_VIDE));
            Assert.IsFalse(bEstValide);
            Assert.IsEmpty(oListeDescriptionFichier);
            Assert.IsTrue(sMessageErreur.Contains(Constants.DATA_MAP_INVALIDE) && sMessageErreur.Contains(Constants.DATA_AVENTURER_INVALIDE) &&
                sMessageErreur.Contains(Constants.DATA_TREASURE_INVALIDE));
            //fichier sans info carte
            (bEstValide, sMessageErreur, oListeDescriptionFichier) = await _oIGestionFichier_BLL.DataFileValideAsync(FICHIER_SANS_INFO_MAP, Path.Combine(REPERTOIRE_FICHIER, FICHIER_SANS_INFO_MAP));
            Assert.IsFalse(bEstValide);
            Assert.IsNotEmpty(oListeDescriptionFichier);
            Assert.IsTrue(sMessageErreur.Contains(Constants.DATA_MAP_INVALIDE));
            //fichier sans info trésor
            (bEstValide, sMessageErreur, oListeDescriptionFichier) = await _oIGestionFichier_BLL.DataFileValideAsync(FICHIER_SANS_TRESOR, Path.Combine(REPERTOIRE_FICHIER, FICHIER_SANS_TRESOR));
            Assert.IsFalse(bEstValide);
            Assert.IsNotEmpty(oListeDescriptionFichier);
            Assert.IsTrue(sMessageErreur.Contains(Constants.DATA_TREASURE_INVALIDE));
            //fichier sans info joueur
            (bEstValide, sMessageErreur, oListeDescriptionFichier) = await _oIGestionFichier_BLL.DataFileValideAsync(FICHIER_SANS_JOUEUR, Path.Combine(REPERTOIRE_FICHIER, FICHIER_SANS_JOUEUR));
            Assert.IsFalse(bEstValide);
            Assert.IsNotEmpty(oListeDescriptionFichier);
            Assert.IsTrue(sMessageErreur.Contains(Constants.DATA_AVENTURER_INVALIDE));
            //test ok
            (bEstValide, sMessageErreur, oListeDescriptionFichier) = await _oIGestionFichier_BLL.DataFileValideAsync(FICHIER_OK, Path.Combine(REPERTOIRE_FICHIER, FICHIER_OK));
            Assert.IsTrue(bEstValide);
            Assert.IsNotEmpty(oListeDescriptionFichier);
            Assert.IsTrue(!sMessageErreur.Contains(Constants.DATA_MAP_INVALIDE) && !sMessageErreur.Contains(Constants.DATA_AVENTURER_INVALIDE) &&
                !sMessageErreur.Contains(Constants.DATA_TREASURE_INVALIDE));
        }
        /// <summary
        /// Test Ecriture return null
        /// Task<string> Ecriture(string sFileName, Jeu oCurrentJeu)
        /// </summary>
        [Test]
        public async Task Ecriture_TestNull()
        {
            //Test sFileName = null
            Assert.IsNull(await _oIGestionFichier_BLL.ExportAsync(null, new Game()));
            //Test sFileName est une chaine vide
            Assert.IsNull(await _oIGestionFichier_BLL.ExportAsync(string.Empty, new Game()));
            //Test oCurrentJeu = null
            Assert.IsNull(await _oIGestionFichier_BLL.ExportAsync(Guid.NewGuid().ToString(), null));
        }
    }
}
