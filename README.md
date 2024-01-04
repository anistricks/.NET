
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

exemple : 
 <Window.Resources>
 <DataTemplate x:Key="listTemplate">
     <StackPanel Margin="0 5 0 5">
         <Label Content="{Binding ProductId}" HorizontalAlignment="Left" VerticalAlignment="Center"/>
         <Label Content="{Binding ProductName}" HorizontalAlignment="Right" VerticalAlignment="Center"/>
     </StackPanel>
 </DataTemplate>
</Window.Resources>
