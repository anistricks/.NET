
".NET"

application wpf

Clic droit sur le projet -> Gérer les packages NuGet 

Ajouter les packages:
Microsoft.EntityFrameworkCore
Microsoft.EntityFrameworkCore.Design
Microsoft.EntityFrameworkCore.Tools
Microsoft.EntityFrameworkCore.SqlServer
Microsoft.EntityFrameworkCore.Proxies

-------------------------------------------------------------------------------------------------------------------------------------
![image](https://github.com/anistricks/.NET/assets/71337903/04856b48-7a7d-4468-91af-63dd3f40b969)

 StartupUri="Views/MainWindow.xaml">

 
Outils – Gestionnaire de Package NuGet -> Console

 Scaffold-DbContext -OutputDir Models 'Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Northwind' Microsoft.EntityFrameworkCore.SqlServer

   puis :
dans protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) ajouter : 


.UseLazyLoadingProxies()
    ("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Northwind;MultipleActiveResultSets=True")




 this.DataContext = new ProductVM(); //exemple
--------------------------------------------------------------------------------------------------------------------------------------
 
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
------------------------------------------------------------------------------------------------------------------------------------
exemple : 
 <Window.Resources>
 </Window.Resources>
 <Window.Resources>
 <DataTemplate x:Key="listTemplate">
     <StackPanel Margin="0 5 0 5">
         <Label Content="{Binding ProductId}" HorizontalAlignment="Left" VerticalAlignment="Center"/>
         <Label Content="{Binding ProductName}" HorizontalAlignment="Right" VerticalAlignment="Center"/>
     </StackPanel>
 </DataTemplate>
</Window.Resources>
!!!!!!!!!!!!!!!!     this.DataContext = new ProductVM(); !!!!!!!!!!!!!!!!!!!!!
-------------------------------------------------------------------------------
a sa faut add  !!! 
 <ListBox x:Name="listBoxProduct" SelectedItem="{Binding SelectedProduct}" ItemsSource="{Binding ProductsList}" ItemTemplate="{StaticResource listboxTemplate}"  Margin="10,24,580,276" />
-------------------------------------------------------------------------------------------------------------------------------------
       <Grid.RowDefinitions>
            <RowDefinition Height="35"/>
            <RowDefinition Height="*" MinHeight="100"/>
            <RowDefinition Height="35"/>
            <RowDefinition Height="200"/>
            <RowDefinition Height="35" MinHeight="35"/>
        </Grid.RowDefinitions>


        <DataGrid Name="dgCustomers" Margin="6" Grid.Row="1" AutoGenerateColumns="False" SelectedItem="{Binding        SelectedEmployee}" ItemsSource="{Binding EmployeesList}" IsReadOnly="True">
            <DataGrid.Columns>
                <!--<DataGridTextColumn Binding="{Binding LastName}" Header="Name" Width="*" />-->
                <DataGridTextColumn Binding="{Binding FullName}" Header="Full Name" Width="*" />
                <DataGridTextColumn Binding="{Binding DisplayBirthDate}" Header="Birth Date" Width="*" />
            </DataGrid.Columns>
        </DataGrid>


        <Label Grid.Row="2" Margin="6,6,87,6" FontSize="12" FontWeight="Bold" >MVVM</Label>
        <Button Command="{Binding AddCommand}" Content="Add" Width="75" Margin="6,6,87,6" Grid.Row="2" HorizontalAlignment="Right"/>
        <Button Content="Remove" Width="75" Margin="6" Grid.Row="2" HorizontalAlignment="Right" />
        <Grid Grid.Row="3" Margin="6" DataContext="{Binding SelectedItem, ElementName=dgCustomers}" >
            <!-- IsEnabled="{Binding SelectedItem, ElementName=dgCustomers, Converter={StaticResource NullToBoolConverter}, ConverterParameter=true}" -->
            <Grid.ColumnDefinitions>
                <ColumnDefinition Width="100"/>
                <ColumnDefinition Width="*"/>
                <ColumnDefinition Width="*"/>
            </Grid.ColumnDefinitions>
            <StackPanel>
                <Label Content="Last Name :" Margin="6" />
                <Label Content="First Name :" Margin="6" />
                <Label Content="Title :" Margin="6" />
                <Label Content="Birth Date :" Margin="6" />
                <Label Content="Hire Date :" Margin="6" />
            </StackPanel>
            <StackPanel Grid.Column="1">
                <TextBox Height="27" Margin="6,6,6,5" Text="{Binding LastName, UpdateSourceTrigger=PropertyChanged}" />
                <TextBox Height="27" Margin="6,6,6,5" Text="{Binding FirstName, UpdateSourceTrigger=PropertyChanged}"/>


                <ComboBox Name="cbTitle" Height="27" Margin="6,6,6,5" 
                          ItemsSource="{Binding DataContext.ListTitle, 
                    RelativeSource={RelativeSource FindAncestor,
                    AncestorType={x:Type Window}}}"                          
                          SelectedItem="{Binding TitleOfCourtesy}" />
                <DatePicker Height="27" Margin="6,6,6,5" SelectedDate="{Binding BirthDate}" />
                <DatePicker Height="27" Margin="6,6,6,5" SelectedDate="{Binding HireDate}" />
            </StackPanel>
            <DataGrid Grid.Column="2" AutoGenerateColumns="False"
                      ItemsSource="{Binding DataContext.OrdersList,
                RelativeSource={RelativeSource FindAncestor,
                AncestorType={x:Type Window}}}">
                <DataGrid.Columns>
                    <DataGridTextColumn Binding="{Binding OrderID}" Header="OrderID" Width="*" />
                    <DataGridTextColumn Binding="{Binding OrderDate}" Header="OrderDate" Width="*" />
                    <DataGridTextColumn Binding="{Binding OrderTotal}" Header="OrderTotal" Width="*" />

                </DataGrid.Columns>
            </DataGrid>
            </Grid>
-------------------------------------------------------------------------------------------------------------------------------------
!!! exemple  !!!

        
-------------------------------------------------------------------------------------------------------------------------------------

  public string DisplayBirthDate
  {
      get { return _employee.BirthDate.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture); }
  }
