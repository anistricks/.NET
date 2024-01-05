
".NET"

application wpf

Clic droit sur le projet -> Gérer les packages NuGet 

Ajouter les packages:
Microsoft.EntityFrameworkCore
Microsoft.EntityFrameworkCore.Design
Microsoft.EntityFrameworkCore.Tools
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Proxies



Outils – Gestionnaire de Package NuGet -> Console

 Scaffold-DbContext -OutputDir Models 'Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Northwind' Microsoft.EntityFrameworkCore.SqlServer

   puis :
dans protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) ajouter : 


.UseLazyLoadingProxies()
    ("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Northwind;MultipleActiveResultSets=True")




 this.DataContext = new ProductVM(); //exemple

 
  : INotifyPropertyChanged
  
  public event PropertyChangedEventHandler PropertyChanged; // La view s'enregistera automatiquement sur cet event

 protected virtual void OnPropertyChanged(string propertyName)
 {
     if (PropertyChanged != null)
     {
         PropertyChanged(this, new PropertyChangedEventArgs(propertyName)); // On notifie que la propriété a changé
     }
 }

!!!! OnPropertyChanged("FullName"); !!!!

exemple : 
 <Window.Resources>
 <DataTemplate x:Key="listTemplate">
     <StackPanel Margin="0 5 0 5">
         <Label Content="{Binding ProductId}" HorizontalAlignment="Left" VerticalAlignment="Center"/>
         <Label Content="{Binding ProductName}" HorizontalAlignment="Right" VerticalAlignment="Center"/>
     </StackPanel>
 </DataTemplate>
</Window.Resources>

a sa faut add  !!! 
 <ListBox x:Name="listBoxProduct" SelectedItem="{Binding SelectedProduct}" ItemsSource="{Binding ListProducts}" ItemTemplate="{StaticResource listboxTemplate}"  Margin="10,24,580,276" />




          <DataGrid  ItemsSource="{Binding EmployeeList}" IsReadOnly="True" AutoGenerateColumns="False" SelectedItem="{Binding SelectedEmployee}"  Margin="0,0,-9,182">
    <DataGrid.Columns>
        <DataGridTextColumn Binding="{Binding FullName}" Header="Full Name" Width="*" />
        <DataGridTextColumn Binding="{Binding DisplayBirthDate}" Header="Birth Date" Width="*" />
    </DataGrid.Columns>
</DataGrid>


  public string DisplayBirthDate
  {
      get { return _employee.BirthDate.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture); }
  }
