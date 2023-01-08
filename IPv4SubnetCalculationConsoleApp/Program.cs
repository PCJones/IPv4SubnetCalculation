using IPv4SubnetCalculationLibrary;

class Programm
{
    static void Main(string[] args)
    {
        while (true)
        {
            IPv4? ipAddress = null;
            IPv4? subnetMask = null;
            while (ipAddress == null)
            {
                Console.WriteLine("IP-Adresse eingeben:");
                var input = Console.ReadLine();
                if (!IPv4.TryCreateIPv4(input, out ipAddress))
                {
                    Console.WriteLine("IP-Adresse hat das falsche Format!");
                }
            }

            while (subnetMask == null)
            {
                Console.WriteLine("Subnetzmaske eingeben:");
                var input = Console.ReadLine();
                if (!IPv4.TryCreateIPv4(input, out subnetMask))
                {
                    Console.WriteLine("Subnetzmaske hat das falsche Format!");
                }
            }

            var networkAddress = IPv4.GetNetworkAddress(ipAddress, subnetMask);
            var possibleClients = IPv4.GetPossibleClientsAmount(subnetMask);

            Console.WriteLine("");
            Console.WriteLine($"Netzwerkadresse: {networkAddress}");
            Console.WriteLine($"Anzahl Clients: {possibleClients}");
            Console.WriteLine("");
            Console.WriteLine("");
        }
    }
}