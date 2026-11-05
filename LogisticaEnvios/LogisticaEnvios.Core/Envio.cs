// Clase Envio
// Representa el dato que se guarda en la tabla hash
public class Envio
{
    public string CodigoRastreo { get; set; }
    public string Remitente { get; set; }
    public string Destinatario { get; set; }
    public string CiudadOrigen { get; set; }
    public string CiudadDestino { get; set; }

    public Envio(string codigoRastreo, string remitente, string destinatario,
                 string ciudadOrigen, string ciudadDestino)
    {
        CodigoRastreo = codigoRastreo;
        Remitente = remitente;
        Destinatario = destinatario;
        CiudadOrigen = ciudadOrigen;
        CiudadDestino = ciudadDestino;
    }

    // Devuelve el envío como una línea del archivo envios.csv
    public string ALineaCsv()
    {
        return CodigoRastreo + "," + Remitente + "," + Destinatario + "," +
               CiudadOrigen + "," + CiudadDestino;
    }
}
