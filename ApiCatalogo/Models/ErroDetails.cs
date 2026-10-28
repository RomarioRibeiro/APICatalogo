namespace ApiCatalogo.Models
{
    public class ErroDetails
    {
        public string StatyusCode { get; set; }

        public string? Menssagem { get; set; }

        public string?  Trace { get; set; }

        public override string ToString()
        {
            return System.Text.Json.JsonSerializer.Serialize(this);
        }
    }
}
