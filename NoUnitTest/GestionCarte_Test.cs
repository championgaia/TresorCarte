using BLL;
using Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Common.Enumerations;

namespace NoUnitTest
{
    public class GestionCarte_Test : NoUnitTestBase
    {
        private IMapManage_BLL _oIGestionCarte_BLL { get; set; }
        [SetUp]
        public void Setup()
        {
            _oIGestionCarte_BLL = new MapManage_BLL(new DataValideManage_BLL());
        }
        /// <summary>
        /// Test CreateCarteTresor return null
        /// Task<TresorCarte> CreateCarteTresor(List<string> oListDescriptionFichier);
        /// </summary>
        [Test]
        public async Task CreateCarteTresor_TestNOk()
        {
            // Test oListDescriptionFichier contient pas info de la carte
            TreasureMap oTresorCarte = await _oIGestionCarte_BLL.CreateTreasureMapAsync(Lists_Donnes_Valide);
            Assert.IsNotNull(oTresorCarte);
            //test nombre de case
            Assert.AreEqual(12, oTresorCarte.ListeCase.Count);
            //test nombre de montagne
            Assert.AreEqual(1, oTresorCarte.ListeCase.Where(c => c.Type == CaseType.Mountain).ToList().Count);
            //test nombre de trésor
            Assert.AreEqual(1, oTresorCarte.ListeCase.Where(c => c.Type == CaseType.Treasure).ToList().Count);
            //test nombre de case occupé
            Assert.AreEqual(1, oTresorCarte.ListeCase.Where(c => c.IsOccupied == true).ToList().Count);
        }
        /// <summary>
        /// Test CreateCarteTresor return null
        /// Task<TresorCarte> CreateCarteTresor(List<string> oListDescriptionFichier);
        /// </summary>
        [Test]
        public async Task CreateCarteTresor_TestNull()
        {
            // Test oListDescriptionFichier = null
            Assert.IsNull(await _oIGestionCarte_BLL.CreateTreasureMapAsync(null));
            // Test oListDescriptionFichier est une collection vide
            Assert.IsNull(await _oIGestionCarte_BLL.CreateTreasureMapAsync(new List<string>()));
            // Test oListDescriptionFichier contient pas info de la carte
            Lists_Donnes_Valide.RemoveAt(Lists_Donnes_Valide.IndexOf(DONNES_CATRE_VALIDE));
            TreasureMap oTresorCarte = await _oIGestionCarte_BLL.CreateTreasureMapAsync(Lists_Donnes_Valide);
            Assert.IsNull(oTresorCarte);
            // Test oListDescriptionFichier contient pas info de la carte
            Lists_Donnes_Valide.Add(DONNES_CATRE_NON_VALIDE);
            oTresorCarte = await _oIGestionCarte_BLL.CreateTreasureMapAsync(Lists_Donnes_Valide);
            Assert.IsNull(oTresorCarte);
        }
        /// <summary>
        /// Test GetValidePosition return false or true
        /// Task<bool> GetValidePosition(TresorCarte oCarte, Position oProchainPrevuPosition)
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task GetValidePosition_TestValidePosition()
        {
            TreasureMap oTresorCarte = await _oIGestionCarte_BLL.CreateTreasureMapAsync(Lists_Donnes_Valide);
            //position ni occupé ni montagne
            Position oProchainPrevuPosition = oTresorCarte.ListeCase.FirstOrDefault(c => !c.IsOccupied && c.Type != CaseType.Mountain).CasePosition;
            bool bResult = await _oIGestionCarte_BLL.GetValidePositionAsync(oTresorCarte, oProchainPrevuPosition);
            Assert.IsTrue(bResult);
            //position occupé
            oProchainPrevuPosition = oTresorCarte.ListeCase.FirstOrDefault(c => c.IsOccupied).CasePosition;
            bResult = await _oIGestionCarte_BLL.GetValidePositionAsync(oTresorCarte, oProchainPrevuPosition);
            Assert.IsFalse(bResult);
            //position montagne
            oProchainPrevuPosition = oTresorCarte.ListeCase.FirstOrDefault(c => c.Type == CaseType.Mountain).CasePosition;
            bResult = await _oIGestionCarte_BLL.GetValidePositionAsync(oTresorCarte, oProchainPrevuPosition);
            Assert.IsFalse(bResult);
            //out of range
            oProchainPrevuPosition = new Position(4, 4);
            bResult = await _oIGestionCarte_BLL.GetValidePositionAsync(oTresorCarte, oProchainPrevuPosition);
            Assert.IsFalse(bResult);
        }
    }
}