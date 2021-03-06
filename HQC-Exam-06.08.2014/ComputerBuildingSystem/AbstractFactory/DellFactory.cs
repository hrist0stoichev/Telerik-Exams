﻿namespace Computers.AbstractFactory
{
    using System.Collections.Generic;

    using global::Computers.ComputerTypes;

    internal class DellFactory : ComputersFactory
    {
        private const int PersonalComputerNumberOfCores = 4;

        private const int PersonalComputerBits = 64;

        private const int PersonalComputerRam = 8;

        private const int PersonalComputerHardDriveCapacity = 1000;

        private const bool PersonalComputerMonochromeVideoCard = false;

        private const int ServerNumberOfCores = 8;

        private const int ServerBits = 64;

        private const int ServerRam = 64;

        private const int ServerHardDriveCapacity = 1000;

        private const bool ServerMonochromeVideoCard = true;

        private const int LaptopNumberOfCores = 4;

        private const int LaptopBits = 32;

        private const int LaptopRam = 8;

        private const int LaptopHardDriveCapacity = 1000;

        private const bool LaptopMonochromeVideoCard = false;

        public override PersonalComputer MakePersonalComputer()
        {
            var ram = new RandomAcessMemory(PersonalComputerRam);
            var videoCard = new VideoCard(PersonalComputerMonochromeVideoCard);
            var motherBoard = new Motherboard(ram, videoCard);
            var cpu = new CentralProcessingUnit(PersonalComputerNumberOfCores, PersonalComputerBits, motherBoard);
            var hardDrive = new HardDriver(PersonalComputerHardDriveCapacity, false, 0);

            var dellPersonalComputer = new PersonalComputer(
                cpu,
                ram,
                new List<HardDriver> { hardDrive },
                videoCard,
                motherBoard);
            return dellPersonalComputer;
        }

        public override Server MakeServer()
        {
            // TODO:make raid
            var ram = new RandomAcessMemory(ServerRam);
            var videoCard = new VideoCard(ServerMonochromeVideoCard);
            var motherBoard = new Motherboard(ram, videoCard);
            var cpu = new CentralProcessingUnit(ServerNumberOfCores, ServerBits, motherBoard);
            var hardDrive = new HardDriver(ServerHardDriveCapacity, false, 0);

            var dellServer = new Server(
                cpu,
                ram,
                new List<HardDriver> { hardDrive },
                videoCard,
                motherBoard);
            return dellServer;
        }

        public override Laptop MakeLaptop()
        {
            var ram = new RandomAcessMemory(LaptopRam);
            var videoCard = new VideoCard(LaptopMonochromeVideoCard);
            var motherBoard = new Motherboard(ram, videoCard);
            var cpu = new CentralProcessingUnit(LaptopNumberOfCores, LaptopBits, motherBoard);
            var hardDrive = new HardDriver(LaptopHardDriveCapacity, false, 0);
            var battery = new LaptopBattery();

            var dellLaptop = new Laptop(
                cpu,
                ram,
                new List<HardDriver> { hardDrive },
                videoCard,
                motherBoard,
                battery);
            return dellLaptop;
        }
    }
}