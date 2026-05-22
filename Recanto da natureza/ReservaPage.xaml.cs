using Recanto_da_natureza.Models;

namespace Recanto_da_natureza;

public partial class ReservaPage : ContentPage
{
    List<Quarto> quartos = new();

    double valorFinal = 0;

    int diarias = 0;

    public ReservaPage()
    {
        InitializeComponent();

        dtCheckin.MinimumDate = DateTime.Today;

        dtCheckout.MinimumDate =
            DateTime.Today.AddDays(1);

        quartos.Add(new Quarto
        {
            Nome = "Chalé Sky",
            ValorAdulto = 450,
            ValorCrianca = 150,
            Imagem = "pousada.jpg",
            Descricao = "Vista panorâmica"
        });

        quartos.Add(new Quarto
        {
            Nome = "Chalé Wood",
            ValorAdulto = 390,
            ValorCrianca = 120,
            Imagem = "wood.jpg",
            Descricao = "Estilo rústico"
        });

        quartos.Add(new Quarto
        {
            Nome = "Chalé Stone",
            ValorAdulto = 520,
            ValorCrianca = 180,
            Imagem = "sky.jpg",
            Descricao = "Design sofisticado"
        });

        pickerQuarto.ItemsSource = quartos;

        pickerQuarto.ItemDisplayBinding =
            new Binding("Nome");
    }

    private void AtualizarReserva(
        object sender,
        EventArgs e)
    {
        lblAdultos.Text =
            $"{(int)stepAdultos.Value} Adulto(s)";

        lblCriancas.Text =
            $"{(int)stepCriancas.Value} Criança(s)";

        if (pickerQuarto.SelectedIndex == -1)
            return;

        if (dtCheckout.Date <= dtCheckin.Date)
            return;

        diarias =
            (dtCheckout.Date - dtCheckin.Date).Days;

        Quarto quarto =
            quartos[pickerQuarto.SelectedIndex];

        double valorAdultos =
            quarto.ValorAdulto *
            stepAdultos.Value;

        double valorCriancas =
            quarto.ValorCrianca *
            stepCriancas.Value;

        valorFinal =
            (valorAdultos + valorCriancas)
            * diarias;

        if (txtCupom.Text?.ToUpper() == "BROTAS10")
        {
            valorFinal *= 0.9;
        }

        lblDiarias.Text =
            $"Diárias: {diarias}";

        lblValor.Text =
            $"Valor Total: {valorFinal:C}";
    }

    private async void ConfirmarReserva(
        object sender,
        EventArgs e)
    {
        if (pickerQuarto.SelectedIndex == -1)
        {
            await DisplayAlert(
                "Erro",
                "Selecione um quarto",
                "OK");

            return;
        }

        if (dtCheckout.Date <= dtCheckin.Date)
        {
            await DisplayAlert(
                "Erro",
                "Data inválida",
                "OK");

            return;
        }

        if (stepAdultos.Value <= 0)
        {
            await DisplayAlert(
                "Erro",
                "Quantidade inválida",
                "OK");

            return;
        }

        Quarto quarto =
            quartos[pickerQuarto.SelectedIndex];

        await Navigation.PushAsync(
            new ReservaConfirmada(
                quarto.Nome,
                diarias,
                valorFinal,
                (int)stepAdultos.Value,
                (int)stepCriancas.Value,
                pickerPagamento.SelectedItem?.ToString() ?? "Pix"
            )
        );
    }
}