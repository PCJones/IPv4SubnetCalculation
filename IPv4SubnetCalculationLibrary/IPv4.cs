using System;

namespace IPv4SubnetCalculationLibrary
{
    public class IPv4
    {
        private readonly byte[] address = new byte[4];

        public IPv4(byte a, byte b, byte c, byte d)
        {
            address[0] = a;
            address[1] = b;
            address[2] = c;
            address[3] = d;
        }

        public IPv4(string? ipAddress)
        {
            if (!IsValidFormat(ipAddress))
            {
                throw new ArgumentException("IP Adresse entspricht nicht dem korrekten Format");
            }

            string[] octets = ipAddress.Split('.');
            for (int i = 0; i < 4; i++)
            {
                address[i] = byte.Parse(octets[i]);
            }
        }

        private static bool IsValidFormat(string? ipAddress)
        {
            if (ipAddress == null)
            {
                return false;
            }

            string[] octets = ipAddress.Split('.');
            if (octets.Length != 4)
            {
                return false;
            }

            foreach (var octet in octets)
            {
                // octet ist integer?
                if (Int32.TryParse(octet, out int number))
                {
                    // octet ist zwischen 0-255?
                    if (number is < 0 or > 255)
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        public static bool TryCreateIPv4(string? ipAddress, out IPv4? ip)
        {
            ip = null;
            try
            {
                ip = new IPv4(ipAddress);
            }
            catch (ArgumentException)
            {
                return false;
            }
            return true;
        }

        public static IPv4 GetNetworkAddress(IPv4 ip, IPv4 subnetMask)
        {
            byte[] networkAddress = new byte[4];
            for (int i = 0; i < 4; i++)
            {
                // AND-Verknüpfung
                networkAddress[i] = (byte)(ip.address[i] & subnetMask.address[i]);
            }
            return new IPv4(networkAddress[0], networkAddress[1], networkAddress[2], networkAddress[3]);
        }

        public static int GetPossibleClientsAmount(IPv4 subnetMask)
        {
            int possibleClients = 1;

            // Die Anzahl möglicher Clients wird für jedes Oktet ausgerechnet und mit der bereits
            // vorhandenen Anzahl möglicher Clients multipliziert 
            for (int i = 0; i < 4; i++)
            {
                possibleClients *= 256 - subnetMask.address[i];
            }

            return possibleClients - 2; // Netzwerk- und Broadcastadresse abziehen
        }

        public override string ToString()
        {
            return $"{address[0]}.{address[1]}.{address[2]}.{address[3]}";
        }
    }
}
