namespace Recanto_da_natureza;

public partial class ReservaConfirmada : ContentPage
{
    public ReservaConfirmada(
        string quarto,
        int diarias,
        double valor,
        int adultos,
        int criancas,
        string pagamento)
    {
        InitializeComponent();

        lblQuarto.Text = $"Quarto: {quarto}";

        lblHospedes.Text =
            $"Hóspedes: {adultos} Adultos e {criancas} Crianças";

        lblDiarias.Text =
            $"Diárias: {diarias}";

        lblPagamento.Text =
            $"Pagamento: {pagamento}";

        lblValor.Text =
            $"Valor Total: {valor:C}";
    }

    private async void NovaReserva(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void VoltarInicio(object sender, EventArgs e)
    {
        await Navigation.PopToRootAsync();
    }
}