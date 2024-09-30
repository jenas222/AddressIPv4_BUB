using System.Net;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class AddressIPv4
{
    private byte[] _address;

    public AddressIPv4(byte[] address)
    {
        if (address.Length != 4)
        {
            throw new ArgumentException("Invalid IPv4 address");
        }

        _address = address;
    }

    public AddressIPv4(string address)
    {
      _address = CheckAndSet(address);
        
    }

    public byte[] GetAddressBytes()
    {
        return _address;
    }

    public string GetAsString()
    {
        return $"{_address[0]}.{_address[1]}.{_address[2]}.{_address[3]}";
    }
    private byte[] CheckAndSet(string value)
    {
        string[] parts = value.Split('.');
        if (parts.Length != 4)
        {
            throw new ArgumentException("Invalid IPv4 address");
        }

        byte[] new_Address = new byte[4];
        for (int i = 0; i < 4; i++)
        {
            if (!byte.TryParse(parts[i], out new_Address[i]))
            {
                throw new ArgumentException($"Invalid IPv4 address part: {parts[i]}");
            }
        }
        return new_Address;
    }
    public AddressIPv4 Set(string value)
    {
        _address = CheckAndSet(value);
        return this;
    }
    public bool IsValid()
    {
        // lze vytvorit jen validni objekty (setem i constructorem)
        return true;
    }

    public override string ToString()
    {
        return GetAsString();
    }
    public int GetAsInt()
    {
        byte[] addressBytes = _address;

        if (addressBytes.Length < 4)
        {
            throw new InvalidOperationException("Byte array is too short to convert to int");
        }

        int addressInt = BitConverter.ToInt32(addressBytes, 0);

        return addressInt;
    }

    public string GetAsBinaryString()
    {
        byte[] addressBytes = _address;

        StringBuilder binaryString = new StringBuilder();

        foreach (byte b in addressBytes)
        {
            string binaryByte = Convert.ToString(b, 2).PadLeft(8, '0');

            binaryString.Append(binaryByte);
        }

        return binaryString.ToString();
    }
}

class Program
{
    static void Main(string[] args)
    {
        byte[] addressBytes = { 192, 168, 1, 1 };
        AddressIPv4 address1 = new AddressIPv4(addressBytes);
        Console.WriteLine($"Address 1: {address1.GetAsString()} - Valid: {address1.IsValid()}");
        try
        {
            address1.Set("1.1.2.3");
            Console.WriteLine($"Address 1: {address1.GetAsString()} - Valid: {address1.IsValid()}");
            address1.Set("1.256.255.5");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        Console.WriteLine($"Address 1: {address1.GetAsInt()} - Valid: {address1.IsValid()}");
        Console.WriteLine($"Address 1: {address1.GetAsBinaryString()} - Valid: {address1.IsValid()}");

        AddressIPv4 address2 = new AddressIPv4("8.8.8.8");
        Console.WriteLine($"Address 2: {address2.GetAsString()} - Valid: {address2.IsValid()}");

        try
        {
            AddressIPv4 address3 = new AddressIPv4("255.1.1.1");
            Console.WriteLine($"Address 3: {address3.GetAsString()} - Valid: {address3.IsValid()}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }

        try
        {
            AddressIPv4 address4 = new AddressIPv4("1.1.1");
            Console.WriteLine($"Address 4: {address4.GetAsString()} - Valid: {address4.IsValid()}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }

        try
        {
            byte[] ninput = [];
            AddressIPv4 address4 = new AddressIPv4(ninput);
            Console.WriteLine($"Address 4: {address4.GetAsString()} - Valid: {address4.IsValid()}");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}