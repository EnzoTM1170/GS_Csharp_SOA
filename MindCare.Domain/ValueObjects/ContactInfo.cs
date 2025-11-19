namespace MindCare.Domain.ValueObjects;

public class ContactInfo
{
    public string Phone { get; private set; }
    public string EmergencyContact { get; private set; }
    public string EmergencyPhone { get; private set; }

    protected ContactInfo() { }

    public ContactInfo(string phone, string emergencyContact, string emergencyPhone)
    {
        if (string.IsNullOrWhiteSpace(phone))
            throw new ArgumentException("Telefone não pode ser vazio", nameof(phone));

        Phone = phone;
        EmergencyContact = emergencyContact ?? string.Empty;
        EmergencyPhone = emergencyPhone ?? string.Empty;
    }
}

