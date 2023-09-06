using System;
using System.Diagnostics.CodeAnalysis;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using static sea_battle_project.PlayerField;
using static sea_battle_project.ComputerField;

namespace Sea_battle_project_visual;

[SuppressMessage("ReSharper", "CommentTypo")]
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
    }
    
    public void ButtonStart_Clicked(object sender, RoutedEventArgs args)
    {
        try
        {
            PlayerFieldCheck();
        }
        catch (Exception e)
        {
            this.FindControl<Button>("ButtonMistakeInShipPlacement")!.Content = "Ошибка в расстановке кораблей!";
            return;
        }
        
        FillTheCompFieldWithShips();
        
        isItPlayerTurn = true;
        this.FindControl<Button>("ButtonMistakeInShipPlacement")!.Content = "";
        this.FindControl<Button>("ButtonPlayerTurn")!.Content = "Ваш ход!";
    }
    
    // пока не работает
    public void ButtonPlayAgain_Clicked(object sender, RoutedEventArgs args)
    {
        ClearTheCompField();
        ClearThePlayerField();
    }
    
    // Клетки игрока
    // 1[1-10]
    public void ButtonP11_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(1, 1, "ButtonP11");
    }
    
    public void ButtonP12_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(1, 2, "ButtonP12");
    }
    public void ButtonP13_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(1, 3, "ButtonP13");
    }
    
    public void ButtonP14_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(1, 4, "ButtonP14");
    }
    public void ButtonP15_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(1, 5, "ButtonP15");
    }
    public void ButtonP16_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(1, 6, "ButtonP16");
    }
    
    public void ButtonP17_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(1, 7, "ButtonP17");
    }
    
    public void ButtonP18_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(1, 8, "ButtonP18");
    }
    
    public void ButtonP19_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(1, 9, "ButtonP19");
    }
    
    public void ButtonP110_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(1, 10, "ButtonP110");
    }
    
    // 2[1-10]
    public void ButtonP21_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(2, 1, "ButtonP21");
    }
    
    public void ButtonP22_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(2, 2, "ButtonP22");
    }
    
    public void ButtonP23_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(2, 3, "ButtonP23");
    }
    
    public void ButtonP24_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(2, 4, "ButtonP24");
    }
    
    public void ButtonP25_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(2, 5, "ButtonP25");
    }
    
    public void ButtonP26_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(2, 6, "ButtonP26");
    }
    
    public void ButtonP27_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(2, 7, "ButtonP27");
    }
    
    public void ButtonP28_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(2, 8, "ButtonP28");
    }
    
    public void ButtonP29_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(2, 9, "ButtonP29");
    }
    
    public void ButtonP210_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(2, 10, "ButtonP210");
    }
    
    //3[1-10]
    public void ButtonP31_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(3, 1, "ButtonP31");
    }
    
    public void ButtonP32_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(3, 2, "ButtonP32");
    }
    
    public void ButtonP33_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(3, 3, "ButtonP33");
    }
    
    public void ButtonP34_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(3, 4, "ButtonP34");
    }
    
    public void ButtonP35_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(3, 5, "ButtonP35");
    }
    
    public void ButtonP36_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(3, 6, "ButtonP36");
    }
    
    public void ButtonP37_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(3, 7, "ButtonP37");
    }
    
    public void ButtonP38_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(3, 8, "ButtonP38");
    }
    
    public void ButtonP39_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(3, 9, "ButtonP39");
    }
    
    public void ButtonP310_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(3, 10, "ButtonP310");
    }
    
    //4[1-10]
    public void ButtonP41_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(4, 1, "ButtonP41");
    }
    
    public void ButtonP42_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(4, 2, "ButtonP42");
    }
    
    public void ButtonP43_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(4, 3, "ButtonP43");
    }
    
    public void ButtonP44_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(4, 4, "ButtonP44");
    }
    
    public void ButtonP45_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(4, 5, "ButtonP45");
    }
    
    public void ButtonP46_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(4, 6, "ButtonP46");
    }
    
    public void ButtonP47_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(4, 7, "ButtonP47");
    }
    
    public void ButtonP48_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(4, 8, "ButtonP48");
    }
    
    public void ButtonP49_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(4, 9, "ButtonP49");
    }
    
    public void ButtonP410_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(4, 10, "ButtonP410");
    }
    
    //5[1-10]
    public void ButtonP51_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(5, 1, "ButtonP51");
    }
    
    public void ButtonP52_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(5, 2, "ButtonP52");
    }
    
    public void ButtonP53_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(5, 3, "ButtonP53");
    }
    
    public void ButtonP54_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(5, 4, "ButtonP54");
    }
    
    public void ButtonP55_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(5, 5, "ButtonP55");
    }
    
    public void ButtonP56_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(5, 6, "ButtonP56");
    }
    
    public void ButtonP57_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(5, 7, "ButtonP57");
    }
    
    public void ButtonP58_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(5, 8, "ButtonP58");
    }
    
    public void ButtonP59_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(5, 9, "ButtonP59");
    }
    
    public void ButtonP510_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(5, 10, "ButtonP510");
    }
    
    //6[1-10]
    public void ButtonP61_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(6, 1, "ButtonP61");
    }
    
    public void ButtonP62_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(6, 2, "ButtonP62");
    }
    
    public void ButtonP63_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(6, 3, "ButtonP63");
    }
    
    public void ButtonP64_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(6, 4, "ButtonP64");
    }
    
    public void ButtonP65_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(6, 5, "ButtonP65");
    }
    
    public void ButtonP66_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(6, 6, "ButtonP66");
    }
    
    public void ButtonP67_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(6, 7, "ButtonP67");
    }
    
    public void ButtonP68_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(6, 8, "ButtonP68");
    }
    
    
    public void ButtonP69_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(6, 9, "ButtonP69");
    }
    
    
    public void ButtonP610_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(6, 10, "ButtonP610");
    }
    
    //7[1-10]
    public void ButtonP71_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(7, 1, "ButtonP71");
    }
    
    public void ButtonP72_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(7, 2, "ButtonP72");
    }
    
    public void ButtonP73_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(7, 3, "ButtonP73");
    }
    
    public void ButtonP74_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(7, 4, "ButtonP74");
    }
    
    public void ButtonP75_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(7, 5, "ButtonP75");
    }
    
    public void ButtonP76_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(7, 6, "ButtonP76");
    }
    
    public void ButtonP77_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(7, 7, "ButtonP77");
    }
    
    public void ButtonP78_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(7, 8, "ButtonP78");
    }
    
    public void ButtonP79_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(7, 9, "ButtonP79");
    }
    
    public void ButtonP710_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(7, 10, "ButtonP710");
    }
    
    //8[1-10]
    public void ButtonP81_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(8, 1, "ButtonP81");
    }
    
    public void ButtonP82_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(8, 2, "ButtonP82");
    }
    
    public void ButtonP83_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(8, 3, "ButtonP83");
    }
    
    public void ButtonP84_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(8, 4, "ButtonP84");
    }
    
    public void ButtonP85_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(8, 5, "ButtonP85");
    }
    
    public void ButtonP86_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(8, 6, "ButtonP86");
    }
    
    public void ButtonP87_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(8, 7, "ButtonP87");
    }
    
    public void ButtonP88_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(8, 8, "ButtonP88");
    }
    
    public void ButtonP89_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(8, 9, "ButtonP89");
    }
    
    public void ButtonP810_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(8, 10, "ButtonP810");
    }
    
    //9[1-10]
    public void ButtonP91_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(9, 1, "ButtonP91");
    }
    
    public void ButtonP92_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(9, 2, "ButtonP92");
    }
    
    public void ButtonP93_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(9, 3, "ButtonP93");
    }
    
    public void ButtonP94_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(9, 4, "ButtonP94");
    }
    
    public void ButtonP95_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(9, 5, "ButtonP95");
    }
    
    public void ButtonP96_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(9, 6, "ButtonP96");
    }
    
    public void ButtonP97_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(9, 7, "ButtonP97");
    }
    
    public void ButtonP98_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(9, 8, "ButtonP98");
    }
    
    public void ButtonP99_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(9, 9, "ButtonP99");
    }
    
    public void ButtonP910_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(9, 10, "ButtonP910");
    }
    
    //10[1-10]
    public void ButtonP101_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(10, 1, "ButtonP101");
    }
    
    public void ButtonP102_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(10, 2, "ButtonP102");
    }
    
    public void ButtonP103_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(10, 3, "ButtonP103");
    }
    
    public void ButtonP104_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(10, 4, "ButtonP104");
    }
    
    public void ButtonP105_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(10, 5, "ButtonP105");
    }
    
    public void ButtonP106_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(10, 6, "ButtonP106");
    }
    
    public void ButtonP107_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(10, 7, "ButtonP107");
    }
    
    public void ButtonP108_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(10, 8, "ButtonP108");
    }
    
    public void ButtonP109_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(10, 9, "ButtonP109");
    }
    
    public void ButtonP1010_Clicked(object sender, RoutedEventArgs args)
    {
        PlayerButtonClicked(10, 10, "ButtonP1010");
    }
    
    // Клетки компьютера
    public void ButtonC11_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(1, 1, "ButtonC11");
    }
    
    public void ButtonC12_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(1, 2, "ButtonC12");
    }
    public void ButtonC13_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(1, 3, "ButtonC13");
    }
    
    public void ButtonC14_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(1, 4, "ButtonC14");
    }
    public void ButtonC15_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(1, 5, "ButtonC15");
    }
    public void ButtonC16_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(1, 6, "ButtonC16");
    }
    
    public void ButtonC17_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(1, 7, "ButtonC17");
    }
    
    public void ButtonC18_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(1, 8, "ButtonC18");
    }
    
    public void ButtonC19_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(1, 9, "ButtonC19");
    }
    
    public void ButtonC110_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(1, 10, "ButtonC110");
    }
    
    // 2[1-10]
    public void ButtonC21_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(2, 1, "ButtonC21");
    }
    
    public void ButtonC22_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(2, 2, "ButtonC22");
    }
    
    public void ButtonC23_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(2, 3, "ButtonC23");
    }
    
    public void ButtonC24_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(2, 4, "ButtonC24");
    }
    
    public void ButtonC25_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(2, 5, "ButtonC25");
    }
    
    public void ButtonC26_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(2, 6, "ButtonC26");
    }
    
    public void ButtonC27_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(2, 7, "ButtonC27");
    }
    
    public void ButtonC28_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(2, 8, "ButtonC28");
    }
    
    public void ButtonC29_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(2, 9, "ButtonC29");
    }
    
    public void ButtonC210_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(2, 10, "ButtonC210");
    }
    
    //3[1-10]
    public void ButtonC31_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(3, 1, "ButtonC31");
    }
    
    public void ButtonC32_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(3, 2, "ButtonC32");
    }
    
    public void ButtonC33_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(3, 3, "ButtonC33");
    }
    
    public void ButtonC34_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(3, 4, "ButtonC34");
    }
    
    public void ButtonC35_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(3, 5, "ButtonC35");
    }
    
    public void ButtonC36_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(3, 6, "ButtonC36");
    }
    
    public void ButtonC37_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(3, 7, "ButtonC37");
    }
    
    public void ButtonC38_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(3, 8, "ButtonC38");
    }
    
    public void ButtonC39_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(3, 9, "ButtonC39");
    }
    
    public void ButtonC310_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(3, 10, "ButtonC310");
    }
    
    //4[1-10]
    public void ButtonC41_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(4, 1, "ButtonC41");
    }
    
    public void ButtonC42_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(4, 2, "ButtonC42");
    }
    
    public void ButtonC43_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(4, 3, "ButtonC43");
    }
    
    public void ButtonC44_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(4, 4, "ButtonC44");
    }
    
    public void ButtonC45_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(4, 5, "ButtonC45");
    }
    
    public void ButtonC46_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(4, 6, "ButtonC46");
    }
    
    public void ButtonC47_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(4, 7, "ButtonC47");
    }
    
    public void ButtonC48_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(4, 8, "ButtonC48");
    }
    
    public void ButtonC49_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(4, 9, "ButtonC49");
    }
    
    public void ButtonC410_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(4, 10, "ButtonC410");
    }
    
    //5[1-10]
    public void ButtonC51_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(5, 1, "ButtonC51");
    }
    
    public void ButtonC52_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(5, 2, "ButtonC52");
    }
    
    public void ButtonC53_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(5, 3, "ButtonC53");
    }
    
    public void ButtonC54_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(5, 4, "ButtonC54");
    }
    
    public void ButtonC55_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(5, 5, "ButtonC55");
    }
    
    public void ButtonC56_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(5, 6, "ButtonC56");
    }
    
    public void ButtonC57_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(5, 7, "ButtonC57");
    }
    
    public void ButtonC58_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(5, 8, "ButtonC58");
    }
    
    public void ButtonC59_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(5, 9, "ButtonC59");
    }
    
    public void ButtonC510_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(5, 10, "ButtonC510");
    }
    
    //6[1-10]
    public void ButtonC61_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(6, 1, "ButtonC61");
    }
    
    public void ButtonC62_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(6, 2, "ButtonC62");
    }
    
    public void ButtonC63_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(6, 3, "ButtonC63");
    }
    
    public void ButtonC64_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(6, 4, "ButtonC64");
    }
    
    public void ButtonC65_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(6, 5, "ButtonC65");
    }
    
    public void ButtonC66_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(6, 6, "ButtonC66");
    }
    
    public void ButtonC67_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(6, 7, "ButtonC67");
    }
    
    public void ButtonC68_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(6, 8, "ButtonC68");
    }
    
    
    public void ButtonC69_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(6, 9, "ButtonC69");
    }
    
    
    public void ButtonC610_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(6, 10, "ButtonC610");
    }
    
    //7[1-10]
    public void ButtonC71_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(7, 1, "ButtonC71");
    }
    
    public void ButtonC72_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(7, 2, "ButtonC72");
    }
    
    public void ButtonC73_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(7, 3, "ButtonC73");
    }
    
    public void ButtonC74_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(7, 4, "ButtonC74");
    }
    
    public void ButtonC75_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(7, 5, "ButtonC75");
    }
    
    public void ButtonC76_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(7, 6, "ButtonC76");
    }
    
    public void ButtonC77_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(7, 7, "ButtonC77");
    }
    
    public void ButtonC78_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(7, 8, "ButtonC78");
    }
    
    public void ButtonC79_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(7, 9, "ButtonC79");
    }
    
    public void ButtonC710_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(7, 10, "ButtonC710");
    }
    
    //8[1-10]
    public void ButtonC81_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(8, 1, "ButtonC81");
    }
    
    public void ButtonC82_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(8, 2, "ButtonC82");
    }
    
    public void ButtonC83_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(8, 3, "ButtonC83");
    }
    
    public void ButtonC84_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(8, 4, "ButtonC84");
    }
    
    public void ButtonC85_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(8, 5, "ButtonC85");
    }
    
    public void ButtonC86_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(8, 6, "ButtonC86");
    }
    
    public void ButtonC87_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(8, 7, "ButtonC87");
    }
    
    public void ButtonC88_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(8, 8, "ButtonC88");
    }
    
    public void ButtonC89_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(8, 9, "ButtonC89");
    }
    
    public void ButtonC810_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(8, 10, "ButtonC810");
    }
    
    //9[1-10]
    public void ButtonC91_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(9, 1, "ButtonC91");
    }
    
    public void ButtonC92_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(9, 2, "ButtonC92");
    }
    
    public void ButtonC93_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(9, 3, "ButtonC93");
    }
    
    public void ButtonC94_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(9, 4, "ButtonC94");
    }
    
    public void ButtonC95_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(9, 5, "ButtonC95");
    }
    
    public void ButtonC96_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(9, 6, "ButtonC96");
    }
    
    public void ButtonC97_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(9, 7, "ButtonC97");
    }
    
    public void ButtonC98_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(9, 8, "ButtonC98");
    }
    
    public void ButtonC99_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(9, 9, "ButtonC99");
    }
    
    public void ButtonC910_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(9, 10, "ButtonC910");
    }
    
    //10[1-10]
    public void ButtonC101_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(10, 1, "ButtonC101");
    }
    
    public void ButtonC102_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(10, 2, "ButtonC102");
    }
    
    public void ButtonC103_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(10, 3, "ButtonC103");
    }
    
    public void ButtonC104_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(10, 4, "ButtonC104");
    }
    
    public void ButtonC105_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(10, 5, "ButtonC105");
    }
    
    public void ButtonC106_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(10, 6, "ButtonC106");
    }
    
    public void ButtonC107_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(10, 7, "ButtonC107");
    }
    
    public void ButtonC108_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(10, 8, "ButtonC108");
    }
    
    public void ButtonC109_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(10, 9, "ButtonC109");
    }
    
    public void ButtonC1010_Clicked(object sender, RoutedEventArgs args)
    {
        ComputerButtonClicked(10, 10, "ButtonC1010");
    }
    
    private void PlayerButtonClicked(int vertIndex, int horizIndex, string buttonName)
    {
        if (playerField[vertIndex, horizIndex] == "1")
        {
            this.FindControl<Button>(buttonName)!.Content = "";
            playerField[vertIndex, horizIndex] = "0";
            return;
        }
        
        this.FindControl<Button>(buttonName)!.Content = "🚢";
        playerField[vertIndex, horizIndex] = "1";
    }
    
    private void ComputerButtonClicked(int vertIndex, int horizIndex, string buttonName)
    {
        this.FindControl<Button>(buttonName)!.Content = "";
        // Клетка уже была выбрана ранее
         if (compField[vertIndex, horizIndex] == "X")
        {
            return;
        }

        if (isItPlayerTurn)
        {
            PlayerHasChosenCell(vertIndex, horizIndex);
        }
    }
    
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
    
    // Реакция на выбор клетки игроком
    private void PlayerHasChosenCell(int vertIndex, int horizIndex)
    {
        // Получаем значение выбранной клетки и дальше работаем уже с ним
        string cellVal = compField[vertIndex, horizIndex];
        
        // Помечаем клетку как обработанную (уже выбранную однажды)
        compField[vertIndex, horizIndex] = "X";
        
        // Если клетка является частью корабля
        if (cellVal.Length == 2)
        {
            // Выводим на экран значение клетки
            this.FindControl<Button>("ButtonC" + vertIndex + horizIndex.ToString())!.Content = "🚢";
            //Console.WriteLine("C" + vertIndex + ", " + horizIndex.ToString() + " : (ship) " + cellVal);
            // Уменьшаем итоговое число ненайденных частей корабля на 1
            compShipsStatus[cellVal[1] - 48, cellVal[0] - 48]--;
            
            // Если были ли найдены все части корабля
            if (compShipsStatus[cellVal[1] - 48, cellVal[0] - 48] == 0)
            {
                // Помечаем клетки вокруг корабля как обработанные и выводим их содержимое на экран 
                MarkCellAroundCompShipAsPicked(cellVal[0] - 48, cellVal);
                // Уменьшаем число оставшихся кораблей на 1
                compShipsCounter--;
                // Если все корабли были найдены
                if (compShipsCounter == 0)
                {
                    // Вывести надпись "Игра окончена" на весь экран 
                    Console.WriteLine("Game Over");
                    isGameOver = true;
                    return;
                    // После чего предложить кнопку "Сыграть ещё раз" или просто очистить всё
                }
            }
            
            // Надпись "Ваш ход" сохраняется
            //Console.WriteLine("Still your turn");
        }
        // Если клетка не является частью корабля
        else
        {
            // Выводим на экран значение клетки
            this.FindControl<Button>("ButtonC" + vertIndex + horizIndex.ToString())!.Content = "💧";
            //Console.WriteLine("C" + vertIndex + ", " + horizIndex.ToString() + " : (drop) " + cellVal);
            isItPlayerTurn = false;
            isItComputerTurn = true;
            // Надпись меняется на "Ход компьютера"
            //Console.WriteLine("Computer turn");
            ComputerIsChoosingCell();
        }
    }

    // Реакция на выбор клетки компьютером
    private void ComputerHasChosenCell(int vertIndex, int horizIndex)
    {
        // Получаем значение выбранной клетки и дальше работаем уже с ним
        string cellVal = playerField[vertIndex, horizIndex];
        
        // Помечаем клетку как обработанную (уже выбранную однажды)
        playerField[vertIndex, horizIndex] = "X";
        
        // Если клетка является частью корабля
        if (cellVal.Length == 2)
        {
            // Выводим на экран значение клетки
            this.FindControl<Button>("ButtonP" + vertIndex + horizIndex.ToString())!.Content = "🚢";
            //Console.WriteLine("P" + vertIndex + ", " + horizIndex.ToString() + " : (ship) " + cellVal);
            // Уменьшаем итоговое число ненайденных частей корабля на 1
            playerShipsStatus[cellVal[1] - 48, cellVal[0] - 48]--;
            
            // Если были ли найдены все части корабля
            if (playerShipsStatus[cellVal[1] - 48, cellVal[0] - 48] == 0)
            {
                // Помечаем клетки вокруг корабля как обработанные и выводим их содержимое на экран 
                MarkCellAroundPlayerShipAsPicked(cellVal[0] - 48, cellVal);
                // Уменьшаем число оставшихся кораблей на 1
                playerShipsCounter--;
                // Если все корабли были найдены
                if (playerShipsCounter == 0)
                {
                    // Вывести надпись "Игра окончена" на весь экран 
                    //Console.WriteLine("Game Over");
                    // После чего предложить кнопку "Сыграть ещё раз" или просто очистить всё
                }
                
                // Очищаем массив с координатами первой и последней найденными клетками корабля
                detectedFirstLastPlayerShipCoords[0, 0] = 0;
                detectedFirstLastPlayerShipCoords[0, 1] = 0;
            }
            // Если первая клетка корабля не была найдена до этого
            else if (detectedFirstLastPlayerShipCoords[0, 0] == 0)
            {
                // Помещаем в массив координаты первой найденной клетки корабля
                detectedFirstLastPlayerShipCoords[0, 0] = vertIndex;
                detectedFirstLastPlayerShipCoords[1, 0] = horizIndex;
                
                // Определяем кординаты клеток, окрущающих эту клетку 
                // (как минимуум одна из них содержит часть корабля)
                // и помещаем их в спецаильный массив
                // Слева
                possibleCells[0, 0] = vertIndex;
                possibleCells[1, 0] = horizIndex - 1;
            
                // Сверху
                possibleCells[0, 1] = vertIndex - 1;
                possibleCells[1, 1] = horizIndex;
            
                // Справа
                possibleCells[0, 2] = vertIndex;
                possibleCells[1, 2] = horizIndex + 1;
            
                // Снизу
                possibleCells[0, 3] = vertIndex + 1;
                possibleCells[1, 3] = horizIndex;
            }
            // Если вторая клетка корабля не была найдена до этого
            else if (detectedFirstLastPlayerShipCoords[0, 1] == 0)
            {
                // Помещаем в массив кординаты второй найденной клетки корабля
                detectedFirstLastPlayerShipCoords[0, 1] = vertIndex;
                detectedFirstLastPlayerShipCoords[1, 1] = horizIndex;
                
                // Вычисляем, в каком направлении (гориз или вертик) и в какой стороне
                // (право/низ  или лево/верх) искать следующую часть корабля
                GetShipDirectionAndSide();
            }
            // Если и первая, и вторая клетки были найдены до этого
            else
            {
                // Помещаем в массив координаты найденной клетки корабря
                detectedFirstLastPlayerShipCoords[0, 1] = vertIndex;
                detectedFirstLastPlayerShipCoords[1, 1] = horizIndex;
            }
            
            // Надпись "Ход Компьютера" сохраняется
            //Console.WriteLine("Still computer turn");
            ComputerIsChoosingCell();
        }
        // Если клетка не является частью корабля
        else
        {
            // Выводим на экран значение клетки
            this.FindControl<Button>("ButtonP" + vertIndex + horizIndex.ToString())!.Content = "💧";
            //Console.WriteLine("P" + vertIndex + ", " + horizIndex.ToString() + " : (drop) " + cellVal);
            isItComputerTurn = false;
            isItPlayerTurn = true;
            // Надпись меняется на "Ход игрока"
            //Console.WriteLine("Your turn");
        }
    }

    // Выясняем положение найденного корабля и в какой стороне искать остальные его части
    private static void GetShipDirectionAndSide()
    {
        // Если верт. коорд. найденных клеток корабля совпадают
        if (detectedFirstLastPlayerShipCoords[0, 0] == detectedFirstLastPlayerShipCoords[0, 1])
        {
            // Корабль расположен вертикально
            isHorizontal = true;
        }

        // Если верт. коорд. первой клетки < верт. коорд. второй клетки (корабль расположен верт.)
        // или гориз. коорд. первой клетки < гориз. коорд. второй клетки (корабль расположен гориз.)
        if (detectedFirstLastPlayerShipCoords[0, 0] < detectedFirstLastPlayerShipCoords[0, 1] ||
            detectedFirstLastPlayerShipCoords[1, 0] < detectedFirstLastPlayerShipCoords[1, 1])
        {
            // Искать след. части корабля справа/внизу
            isToLeftOrTop = false;
        }
    }
    
    // Помечает клетки вокруг найденного полностью корабля как обработанные ("Х")
    // и выводит их содержимое (то, что они пусты) на экран
    private void MarkCellAroundCompShipAsPicked(int shipLength, string shipName)
    {
        // Порядковый номер корабля (ниже написано по какому принципу между ними распределяются цифры от 1 до 10)
        int shipNum = GetShipNum(shipName);
        // Проходимся по прямоугольнику, состояющему из клеток вокруг корабля и самого корабля
        int vertIndex = compShipsCoordinates[shipNum,0, 1] - 1;
        while (vertIndex <= compShipsCoordinates[shipNum, 0, shipLength] + 1)
        {
            int horizIndex = compShipsCoordinates[shipNum, 1, 1] - 1;
            while (horizIndex <= compShipsCoordinates[shipNum, 1, shipLength] + 1)
            {
                // Выбранная клетка не является частью корабля или не была обработана раньше
                if (compField[vertIndex, horizIndex] is not "X")
                {
                    // For checking myself
                    string cellVal = compField[vertIndex, horizIndex];
                    // Помечаем клетку как обработанную
                    compField[vertIndex, horizIndex] = "X"; 
                    // Выводим на экран её значение
                    this.FindControl<Button>("ButtonC" + vertIndex + horizIndex.ToString())!.Content = "💧";
                    //Console.WriteLine("C" + vertIndex + ", " + horizIndex.ToString() + " : (drop) " + cellVal);
                }

                horizIndex++;
            }

            vertIndex++;
        }
    }
    
    // Помечает клетки вокруг найденного полностью корабля как обработанные ("Х")
    // и выводит их содержимое (то, что они пусты) на экран
    private void MarkCellAroundPlayerShipAsPicked(int shipLength, string shipName)
    {
        // Порядковый номер корабля (ниже написано по какому принципу между ними распределяются цифры от 1 до 10)
        int shipNum = GetShipNum(shipName);
        // Прозодимся по прямоугольнику, состояющему из клеток вокруг корабля и самого корабля
        int vertIndex = playerShipsCoordinates[shipNum,0, 1] - 1;
        while (vertIndex <= playerShipsCoordinates[shipNum, 0, shipLength] + 1)
        {
            int horizIndex = playerShipsCoordinates[shipNum, 1, 1] - 1;
            while (horizIndex <= playerShipsCoordinates[shipNum, 1, shipLength] + 1)
            {
                // Выбранная клетка не является частью корабля или уже была обработана раньше
                if (playerField[vertIndex, horizIndex] is not "X")
                {
                    // For checking myself
                    string cellVal = playerField[vertIndex, horizIndex];
                    // Помечаем клетку как обработанную
                    playerField[vertIndex, horizIndex] = "X"; 
                    // Выводим на экран её значение
                    this.FindControl<Button>("Button" + vertIndex + horizIndex.ToString())!.Content = "💧";
                    //Console.WriteLine("P" + vertIndex + ", " + horizIndex.ToString() + " : (drop) " + cellVal);
                }

                horizIndex++;
            }

            vertIndex++;
        }
    }

    // Выбор клетки компьютером
    private void ComputerIsChoosingCell()
    {
        // Первая клетка корабля не была найдена
        if (detectedFirstLastPlayerShipCoords[0, 0] == 0)
        {
            // Рандомно выбираем клетку
            int vertIndex = randomizer.Next(1, 11);
            int horizIndex = randomizer.Next(1, 11);

            // Если найденная клетка уже была обработана, выбираем другую.
            // И так пока не найдём не обработанную раньше
            while (playerField[vertIndex, horizIndex] == "X")
            {
                vertIndex = randomizer.Next(1, 11);
                horizIndex = randomizer.Next(1, 11);
            }
            
            ComputerHasChosenCell(vertIndex, horizIndex);
        }
        // Вторая клетка корабля не была найдена
        else if (detectedFirstLastPlayerShipCoords[0, 1] == 0)
        {
            // Выбираем клетку из массива клеток подозрительных на хранение части корабля
            // (Выбираем её с помощью выбора номера клетки по счёту в массиве)
            int possibleCellIndex = randomizer.Next(0, possibleCellsNum);
            // Получаем координаты клетки (Выбор сделан)
            int vertIndex = possibleCells[0, randomizer.Next(0, possibleCellIndex)];
            int horizIndex = possibleCells[1, randomizer.Next(0, possibleCellIndex)];
            
            // Меняем (только что) выбранную клетку с ещё не проверенной первой с конца
            // (то есть не той, что уже была выбрана раньше и оказалась в конце)
            // За этим помогает сделить число оставшихся непроверенныъ клеток
            (possibleCells[0, possibleCellIndex], possibleCells[0, possibleCellsNum - 1]) =
                (possibleCells[0, possibleCellsNum - 1], possibleCells[0, possibleCellIndex]);
            (possibleCells[1, possibleCellIndex], possibleCells[1, possibleCellsNum - 1]) =
                (possibleCells[1, possibleCellsNum - 1], possibleCells[1, possibleCellIndex]);
            // Уменьшаем число непроверенных клеток подозрительных на хранение части корабля
            possibleCellsNum--;

            // Если выбранная клетка уже была обработана раньше
            // (= часть корабля скрывается в другой клетке из массива)
            if (playerField[vertIndex, horizIndex] == "X")
            {
                // Пробуем ещё раз (с другой клеткой из массива клеток подозрительных на хранение части корабля
                ComputerIsChoosingCell();
                return;
            }
            
            // Передаём координаты клетки в функцию, которая её обрабатывает
            ComputerHasChosenCell(vertIndex, horizIndex);
        }
        // Ищем оставшиеся части корабля
        else
        {
            // Выбираем следующую клетку, которую будем проверять, опираясь на выясенные ранее
            // положение корабля и сторону, в которой стоит искать след. части корабля
            int vertIndex = isSideChangeHappened? possibleCells[0, 0] : possibleCells[0, 1];
            int horizIndex = isSideChangeHappened? possibleCells[1, 0] : possibleCells[1, 1];
            int sideSign = isToLeftOrTop ? -1 : 1;

            vertIndex = isHorizontal ? vertIndex : vertIndex + sideSign;
            horizIndex = isHorizontal ? horizIndex + sideSign : horizIndex;
            
            // Если выбранная клетка уже была обработана раньше
            // (= клетка с частью корабля скрывается с другой стороны)
            if (playerField[vertIndex, horizIndex] == "X")
            {
                // Пробуем ещё раз, но теперь проверяем клетки с другой стороны
                isSideChangeHappened = true;
                isToLeftOrTop = !isToLeftOrTop;
                ComputerIsChoosingCell();
                return;
            }
            
            // Передаём координаты клетки в функцию, которая её обрабатывает
            ComputerHasChosenCell(vertIndex, horizIndex);
        }
    }
    
    // Заполняет клетки поля игрока "0"-ми (индекаторам того, что клетка пуста)
    // и очищает (делает пустыми) клетки поля игрока на экране
    private void ClearThePlayerField()
    {
        for (int vertIndex = 1; vertIndex <= 10; vertIndex++)
        {
            for (int horizIndex = 1; horizIndex <= 10; horizIndex++)
            {
                playerField[vertIndex, horizIndex] = "0";
                this.FindControl<Button>("ButtonP" + vertIndex + horizIndex.ToString())!.Content = "";
            }
        }
    }
    
    // Заполняет клетки поля корабля "0"-ми (индекаторам того, что клетка пуста)
    // и очищает (делает пустыми) клетки поля компьютера на экране
    private void ClearTheCompField()
    {
        for (int vertIndex = 1; vertIndex <= 10; vertIndex++)
        {
            for (int horizIndex = 1; horizIndex <= 10; horizIndex++)
            {
                compField[vertIndex, horizIndex] = "0";
                this.FindControl<Button>("ButtonС" + vertIndex + horizIndex.ToString())!.Content = "";
            }
        }
    }
    
    // Положение корабля (верт. или гориз.)
    private static bool isHorizontal = false;
    // С какой стороны искать клетки
    private static bool isToLeftOrTop = true;
    // Менялась ли в процессе сторона, в которой искать клетки
    private static bool isSideChangeHappened = false;
    
    // Массив, хранящий клетки подозрительные на хранение части корабля
    private static int[,] possibleCells = new int[2, 4];
    // Число непроверенных клеток подозрительных на хранение части корабля
    private static int possibleCellsNum = 4;
    
    // Хранит координаты клетки корабля найденной первой и последней (на текущий момент,
    // != что является последней клеткой корабля, который ищется)
    private static int[,] detectedFirstLastPlayerShipCoords = new int[2, 2];
    
    // Первым ходит игрок
    private static bool isItPlayerTurn = true;
    private static bool isItComputerTurn = false;
    private static bool isGameOver = false;

}