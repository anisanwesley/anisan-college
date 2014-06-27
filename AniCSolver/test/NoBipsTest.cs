using System;
using AniCSolver.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AniCSolver.Test
{
    [TestClass]
    public class NoBipsTest
    {
        private Sistema _sistema;

        [TestInitialize]
        public void Init()
        {
            _sistema = new Sistema("c:\\temp\\base.txt", "c:\\temp\\codigos.txt");

        }

        [TestMethod]
        public void T1_Resolvido()
        {
            _sistema.Responder("s" //liga?
                );
            Assert.AreEqual("Resolvido", _sistema.Solucao);
        }

        [TestMethod]
        public void T3_Corrigir_Autofalante()
        {
            _sistema.Responder("n", //liga? 
                "n", //bip?
                "n" //autofalante
                );
            Assert.AreEqual("Corrigir Autofalante", _sistema.Solucao);
        }

        [TestMethod]
        public void T3_Trocar_Cabo_de_Energia()
        {
            _sistema.Responder("n", //liga?
                "n", //bip?
                "s", //autofalante?
                "n", //tensao cabo?
                "s" //tensao estab?
                );
            Assert.AreEqual("Trocar Cabo de Energia", _sistema.Solucao);
        }

        [TestMethod]
        public void T4_Trocar_Estabilizador()
        {
            _sistema.Responder("n", //liga?
                "n", //bip?
                "s", //autofalante?
                "n", //tensao cabo?
                "n", //tensao estab?
                "s" //tensao tomada?
                );
            Assert.AreEqual("Trocar Estabilizador", _sistema.Solucao);
        }

        [TestMethod]
        public void T5_Verificar_Tomada_ou_Disjuntor()
        {
            _sistema.Responder("n", //liga?
                "n", //bip?
                "s", //autofalante?
                "n", //tensao cabo?
                "n", //tensao estab?
                "n" //tensao tomada?
                );
            Assert.AreEqual("Verificar Tomada ou Disjuntor", _sistema.Solucao);
        }

        [TestMethod]
        public void T6_Corrigir_Posição_da_Chave_Seletora()
        {
            _sistema.Responder("n", //liga?
                "n", //bip?
                "s", //autofalante?
                "s", //tensao cabo?
                "n" //chave seletora?
                );
            Assert.AreEqual("Corrigir Posição da Chave Seletora", _sistema.Solucao);
        }

        [TestMethod]
        public void T7_Trocar_Memórias()
        {
            _sistema.Responder("n", //liga?
                "n", //bip?
                "s", //autofalante?
                "s", //tensao cabo?
                "s", //chave seletora?
                "n" //memorias ok?
                );
            Assert.AreEqual("Trocar Memórias", _sistema.Solucao);
        }

        [TestMethod]
        public void T8_Limpar_Placa_de_Video()
        {
            _sistema.Responder("n", //liga?
                "n", //bip?
                "s", //autofalante?
                "s", //tensao cabo?
                "s", //chave seletora?
                "s", //memorias ok?
                "s" //pci-express?
                );
            Assert.AreEqual("Limpar Placa de Video", _sistema.Solucao);
        }

        [TestMethod]
        public void T9_Trocar_HD()
        {
            _sistema.Responder("n", //liga?
                "n", //bip?
                "s", //autofalante?
                "s", //tensao cabo?
                "s", //chave seletora?
                "s", //memorias ok?
                "n", //pci-express?
                "n" //hd?
                );
            Assert.AreEqual("Trocar HD", _sistema.Solucao);
        }

        [TestMethod]
        public void T10_Trocar_Floppy()
        {
            _sistema.Responder("n", //liga?
                "n", //bip?
                "s", //autofalante?
                "s", //tensao cabo?
                "s", //chave seletora?
                "s", //memorias ok?
                "n", //pci-express?
                "s", //hd?
                "n" //floppy?
                );
            Assert.AreEqual("Trocar Floppy", _sistema.Solucao);
        }

        [TestMethod]
        public void T11_Trocar_Leitor_de_DVD()
        {
            _sistema.Responder("n", //liga?
                "n", //bip?
                "s", //autofalante?
                "s", //tensao cabo?
                "s", //chave seletora?
                "s", //memorias ok?
                "n", //pci-express?
                "s", //hd?
                "s", //floppy?
                "n" //dvd?
                );
            Assert.AreEqual("Trocar Leitor de DVD", _sistema.Solucao);
        }

        [TestMethod]
        public void T12_Trocar_Bateria()
        {
            _sistema.Responder("n", //liga?
                "n", //bip?
                "s", //autofalante?
                "s", //tensao cabo?
                "s", //chave seletora?
                "s", //memorias ok?
                "n", //pci-express?
                "s", //hd?
                "s", //floppy?
                "s", //dvd?
                "n" //bateria?
                );
            Assert.AreEqual("Trocar Bateria", _sistema.Solucao);
        }

        [TestMethod]
        public void T13_Trocar_Capacitores()
        {
            _sistema.Responder("n", //liga?
                "n", //bip?
                "s", //autofalante?
                "s", //tensao cabo?
                "s", //chave seletora?
                "s", //memorias ok?
                "n", //pci-express?
                "s", //hd?
                "s", //floppy?
                "s", //dvd?
                "s", //bateria?
                "s" //capacitor estufado?
                );
            Assert.AreEqual("Trocar Capacitores", _sistema.Solucao);
        }

        [TestMethod]
        public void T14_Trocar_Processador()
        {
            _sistema.Responder("n", //liga?
                "n", //bip?
                "s", //autofalante?
                "s", //tensao cabo?
                "s", //chave seletora?
                "s", //memorias ok?
                "n", //pci-express?
                "s", //hd?
                "s", //floppy?
                "s", //dvd?
                "s", //bateria?
                "n", //capacitor estufado?
                "n" //processador confiavel?
                );
            Assert.AreEqual("Trocar Processador", _sistema.Solucao);
        }

        [TestMethod]
        public void T15_Corrigir_Jumper()
        {
            _sistema.Responder("n", //liga?
                "n", //bip?
                "s", //autofalante?
                "s", //tensao cabo?
                "s", //chave seletora?
                "s", //memorias ok?
                "n", //pci-express?
                "s", //hd?
                "s", //floppy?
                "s", //dvd?
                "s", //bateria?
                "n", //capacitor estufado?
                "s", //processador confiavel?
                "n" //jumper ok?
                );
            Assert.AreEqual("Corrigir Jumper", _sistema.Solucao);
        }

        [TestMethod]
        public void T16_Trocar_Jumper()
        {
            _sistema.Responder("n", //liga?
                "n", //bip?
                "s", //autofalante?
                "s", //tensao cabo?
                "s", //chave seletora?
                "s", //memorias ok?
                "n", //pci-express?
                "s", //hd?
                "s", //floppy?
                "s", //dvd?
                "s", //bateria?
                "n", //capacitor estufado?
                "s", //processador confiavel?
                "s", //jumper ok?
                "s" //partida direta?
                );
            Assert.AreEqual("Trocar Jumper", _sistema.Solucao);
        }

        [TestMethod]
        public void T17_Trocar_Placa_mae()
        {
            _sistema.Responder("n", //liga?
                "n", //bip?
                "s", //autofalante?
                "s", //tensao cabo?
                "s", //chave seletora?
                "s", //memorias ok?
                "n", //pci-express?
                "s", //hd?
                "s", //floppy?
                "s", //dvd?
                "s", //bateria?
                "n", //capacitor estufado?
                "s", //processador confiavel?
                "s", //jumper ok?
                "n" //partida direta?
                );
            Assert.AreEqual("Trocar Placa-mãe", _sistema.Solucao);
        }
    }
}