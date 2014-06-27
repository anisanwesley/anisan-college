using System;
using AniCSolver.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AniCSolver.Test
{
    [TestClass]
    public class BipsTest
    {
        private Sistema _sistema;

        [TestInitialize]
        public void Init()
        {
            _sistema = new Sistema("c:\\temp\\base.txt", "c:\\temp\\codigos.txt");

        }
        [TestMethod]
        public void BA_Limpar_Placa_de_Video()
        {
            _sistema.Responder("n", //liga? 
                "s", //bip?
                "ambos" //tipo de bip
                );
            Assert.AreEqual("Limpar Placa de Video", _sistema.Solucao);
        }
        [TestMethod]
        public void BL1_Limpar_Memorias()
        {
            _sistema.Responder("n", //liga? 
                "s", //bip?
                "longos", //tipo de bip
                "1 bip longo" //quantidade
                );
            Assert.AreEqual("Limpar Memorias", _sistema.Solucao);
        }
        [TestMethod]
        public void BL2_Desativar_Parity_Check_no_Setup()
        {
            _sistema.Responder("n", //liga? 
                "s", //bip?
                "longos", //tipo de bip
                "2 bips longo", //quantidade
                "s" //parity check
                );
            Assert.AreEqual("Desativar Parity Check no Setup", _sistema.Solucao);
        }
        [TestMethod]
        public void BL3_Desativar_Parity_Check_no_Setup()
        {
            _sistema.Responder("n", //liga? 
                "s", //bip?
                "longos", //tipo de bip
                "3 bips longo", //quantidade
                "s" //parity check
                );
            Assert.AreEqual("Desativar Parity Check no Setup", _sistema.Solucao);
        }
        [TestMethod]
        public void BL2_Limpar_Memorias()
        {
            _sistema.Responder("n", //liga? 
                "s", //bip?
                "longos", //tipo de bip
                "2 bips longo", //quantidade
                "n" //parity check
                );
            Assert.AreEqual("Limpar Memorias", _sistema.Solucao);
        }
        [TestMethod]
        public void BL3_Limpar_Memorias()
        {
            _sistema.Responder("n", //liga? 
                "s", //bip?
                "longos", //tipo de bip
                "3 bips longo", //quantidade
                "n" //parity check
                );
            Assert.AreEqual("Limpar Memorias", _sistema.Solucao);
        }
        [TestMethod]
        public void BL4_Aspirar_Slots_das_memorias()
        {
            _sistema.Responder("n", //liga? 
                "s", //bip?
                "longos", //tipo de bip
                "4 bips longo" //quantidade
                );
            Assert.AreEqual("Aspirar Slots das memorias", _sistema.Solucao);
        }
        [TestMethod]
        public void BC1_Verificar_Conectores_de_Vídeo()
        {
            _sistema.Responder("n", //liga? 
                "s", //bip?
                "curtos", //tipo de bip
                "1 bip curto", //quantidade
                "s" //monitor ok
                );
            Assert.AreEqual("Verificar Conectores de Vídeo", _sistema.Solucao);
        }
        [TestMethod]
        public void BC1_Trocar_Monitor()
        {
            _sistema.Responder("n", //liga? 
                "s", //bip?
                "curtos", //tipo de bip
                "1 bip curto", //quantidade
                "n" //monitor ok
                );
            Assert.AreEqual("Trocar Monitor", _sistema.Solucao);
        }
        [TestMethod]
        public void BC2_Verificar_Conectores_da_Placa_mãe()
        {
            _sistema.Responder("n", //liga? 
                "s", //bip?
                "curtos", //tipo de bip
                "2 bips curtos" //quantidade
                );
            Assert.AreEqual("Verificar Conectores da Placa-mãe", _sistema.Solucao);
        }
        [TestMethod]
        public void BC5_Trocar_Processador()
        {
            _sistema.Responder("n", //liga? 
                "s", //bip?
                "curtos", //tipo de bip
                "5 bips curtos", //quantidade
                "s" //zif
                );
            Assert.AreEqual("Trocar Processador", _sistema.Solucao);
        }
        [TestMethod]
        public void BC5_Corrigir_Alavanaca_Zif()
        {
            _sistema.Responder("n", //liga? 
                "s", //bip?
                "curtos", //tipo de bip
                "5 bips curtos", //quantidade
                "n" //zif
                );
            Assert.AreEqual("Corrigir Alavanaca Zif", _sistema.Solucao);
        }
        [TestMethod]
        public void BC6_Trocar_Chipset()
        {
            _sistema.Responder("n", //liga? 
                "s", //bip?
                "curtos", //tipo de bip
                "6 bips curtos" //quantidade
                );
            Assert.AreEqual("Trocar Chipset", _sistema.Solucao);
        }
        [TestMethod]
        public void BC7_Corrigir_Frequencia_do_Processador()
        {
            _sistema.Responder("n", //liga? 
                "s", //bip?
                "curtos", //tipo de bip
                "7 bips curtos", //quantidade
                "s" //overclock
                );
            Assert.AreEqual("Corrigir Frequencia do Processador", _sistema.Solucao);
        }
        [TestMethod]
        public void BC8_Limpar_Placa_de_Video()
        {
            _sistema.Responder("n", //liga? 
                "s", //bip?
                "curtos", //tipo de bip
                "8 bips curtos" //quantidade
                );
            Assert.AreEqual("Limpar Placa de Video", _sistema.Solucao);
        }
        [TestMethod]
        public void BC9_Atualizar_BIOS()
        {
            _sistema.Responder("n", //liga? 
                "s", //bip?
                "curtos", //tipo de bip
                "9 bips curtos", //quantidade
                "n" //bios atualizada
                );
            Assert.AreEqual("Atualizar BIOS", _sistema.Solucao);
        }
        [TestMethod]
        public void BC9_Trocar_BIOS()
        {
            _sistema.Responder("n", //liga? 
                "s", //bip?
                "curtos", //tipo de bip
                "9 bips curtos", //quantidade
                "s", //bios atualizada
                "s" //bateria ok
                );
            Assert.AreEqual("Trocar BIOS", _sistema.Solucao);
        }
        [TestMethod]
        public void BC9_Trocar_Bateria()
        {
            _sistema.Responder("n", //liga? 
                "s", //bip?
                "curtos", //tipo de bip
                "9 bips curtos", //quantidade
                "s", //bios atualizada
                "n" //bateria ok
                );
            Assert.AreEqual("Trocar Bateria", _sistema.Solucao);
        }
        [TestMethod]
        public void BC10_Trocar_Placa_mãe()
        {
            _sistema.Responder("n", //liga? 
                "s", //bip?
                "curtos", //tipo de bip
                "10 bips curtos" //quantidade
                );
            Assert.AreEqual("Trocar Placa-mãe", _sistema.Solucao);
        }
        [TestMethod]
        public void BC11_Aumentar_Timeout_do_Cache()
        {
            _sistema.Responder("n", //liga? 
                "s", //bip?
                "curtos", //tipo de bip
                "11 bips curtos" //quantidade
                );
            Assert.AreEqual("Aumentar Timeout do Cache", _sistema.Solucao);
        }
    }
}
