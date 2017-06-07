using ROBOTIS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinectBioloid.RobotLayer
{
    public class Robot : IRobot
    {
        private IPositionChecker _positionChecker;


        public const int DEFAULT_PORTNUM = 5; // COM3
        public const int TIMEOUT_TIME = 1000; // msec

        public Robot(IPositionChecker positionChecker)
        {

            if (zigbee.zgb_initialize(DEFAULT_PORTNUM) == 0)
            {
                Console.WriteLine("Failed to open Zig2Serial!");
                Console.WriteLine("Press any key to terminate...");
                Console.ReadKey(true);
                return;
            }
            else
                Console.WriteLine("Succeed to open Zig2Serial!");

            _positionChecker = positionChecker;
            _positionChecker.MatchFound += _positionChecker_MatchFound;
        }

        private void _positionChecker_MatchFound(object sender, MatchFoundEventArgs e)
        {
            var leftHandPosition = e.LeftHandPosition;
            var rightHandPosition = e.RightHandPosition;

            if (leftHandPosition == ActionPointsEnum.LeftUp && rightHandPosition == ActionPointsEnum.RightUp)
            {
                if (zigbee.zgb_tx_data(1) == 0)
                    Console.WriteLine("Failed to transmit");
                Console.WriteLine("Gora-Gora");
            }

            if (leftHandPosition == ActionPointsEnum.LeftMid && rightHandPosition == ActionPointsEnum.RightMid)
            {
                if (zigbee.zgb_tx_data(3) == 0)
                    Console.WriteLine("Failed to transmit");
                Console.WriteLine("Srodek-Srodek");
            }

            if (leftHandPosition == ActionPointsEnum.LeftDown && rightHandPosition == ActionPointsEnum.RightDown)
            {
                if (zigbee.zgb_tx_data(2) == 0)
                    Console.WriteLine("Failed to transmit");
                Console.WriteLine("Dol-Dol");
            }

            if (leftHandPosition == ActionPointsEnum.LeftUp && rightHandPosition == ActionPointsEnum.RightMid)
            {
                if (zigbee.zgb_tx_data(4) == 0)
                    Console.WriteLine("Failed to transmit");
                Console.WriteLine("Gora-Srodek");
            }

            if (leftHandPosition == ActionPointsEnum.LeftMid && rightHandPosition == ActionPointsEnum.RightUp)
            {
                if (zigbee.zgb_tx_data(8) == 0)
                    Console.WriteLine("Failed to transmit");
                Console.WriteLine("Srodek-Gora");
            }

            if (leftHandPosition == ActionPointsEnum.LeftMid && rightHandPosition == ActionPointsEnum.RightDown)
            {
                if (zigbee.zgb_tx_data(16) == 0)
                    Console.WriteLine("Failed to transmit");
                Console.WriteLine("Srodek-Dol");
            }

            if (leftHandPosition == ActionPointsEnum.LeftDown && rightHandPosition == ActionPointsEnum.RightMid)
            {
                if (zigbee.zgb_tx_data(64) == 0)
                    Console.WriteLine("Failed to transmit");
                Console.WriteLine("Dol-Srodek");
            }

        }
    }
}
