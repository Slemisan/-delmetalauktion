   M S B A M L   `   `   `  ����F  BMetalWpfApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:MetalWpfAppMetalWpfApp  5   	       MetalWpfApp.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:MetalWpfApp     5   	   -   5          � Title$(  "Systemprogram for Metalauktion A/S��5   	   $	��460��64   $��679.333��6A    � 
ResizeMode$ NoResize=�6Q   .��5	      � $��0,0,4,0q�6   .+�5
   
   &   System.Windows.Controls.DataGrid  -   5          Name$ SalgsudbudGrid��5
         AutoGenerateColumns .     AlternatingRowBackground
 ����� �� AlternationCount$ 2��6h      CanUserAddRows .     CanUserDeleteRows .     
IsReadOnly . $��	0,0,5,161q�6�   	   Columns	 5      0  *System.Windows.Controls.DataGridTextColumn  5      -   5        MetalColumn��,  &System.Windows.Controls.DataGridColumn
   Header$
 	Metaltype��5   X      Width5  /System.Windows.Controls.DataGridLengthConverter$ 70 6k      CanUserResize .  1  +System.Windows.Controls.DataGridBoundColumn   Binding 6:   �� *	MetalType+6v     5      -   5        
GramColumn��$
 	Vægt i g��5   R   $ 55 6d    .   69   �� *Gram+6o     5      -   5        TidsfristColumn��$5
 /Tid tilbage (Dag - Timer - Minutter - Sekunder)��5   \   $ * 6�    .   6>   �� *	Tidsfrist+6�     5      -   5        Bud��$
 Højeste bud��5   K    .   62   �� *Pris+6`   5      �� 5   
   $��Center=�6   $��10,275,283,0q�61   $
��Wrap=�6G   $&�� Opret nyt salgsudbud til auktion��6[   $	��Top=�6�   $��15(�6�   $	��364��6�   �� 5   
   $
��Left=�6   $��30��6/   $	��146��6;   $��10,313,0,88q�6G   $
��Wrap=�6\   $��Vælg metaltype:��6p   $��Center=�6�   �� 5   
   -   5         ��Name$ MetalComboBox��5      $
��Left=�6*   $��30��6E   $	��170��6Q   $��161,313,0,0q�6]   $	��Top=�6r   �� 5   
   $
��Left=�6   $��30��6/   $	��146��6;   $��10,348,0,53q�6G   $
��Wrap=�6\   $��Indtast vægt i gram:��6p   $��Center=�6�   �� 5   
   -   5         ��Name$ GramTextBox��5      $
��Left=�6'   $��30��6B   $	��170��6N   $��161,348,0,0q�6Z    �� TextWrapping$
 Wrap=�6o   $	��Top=�6�   �� 5   
   $
��Left=�6   $��30��6/   $	��146��6;   $��336,313,0,88q�6G   $
��Wrap=�6]   $��Vælg udløbsdato:��6q   $��Center=�6�   (  "System.Windows.Controls.DatePicker  5   
   -	   5          Name$ UdløbDatePicker��5      $
��Left=�6.   $��30��6I   $	��170��6U   $��482,313,0,0q�6a   $	��Top=�6v   �� 5   
   $
��Left=�6   $��30��6/   $	��146��6;   $��336,348,0,53q�6G   $
��Wrap=�6]   $��Vælg udløbstidspunkt:��6q   $��Center=�6�   �� 5    
   -
   5        $ TimeComboBox��5       $
��Left=�6)   $��482,348,0,0q�6D   $	��Top=�6Y   $��30��6q   $��80��6}   �� 5"   
   $	��  :��6   $
��Left=�6   $��554,348,0,0q�69   $	��Top=�6N   $��30��6f   $��30��6r   �� 5#   
   -   5        $ MinutComboBox��5#      $
��Left=�6*   $��572,348,0,0q�6E   $	��Top=�6Z   $��30��6r   $��80��6~   �� 5&   
   -   5        $��Opret��5&      $
��Left=�6!   $��
10,391,0,0q�6<   $	��Top=�6P   $	��642��6h   $��30��6t   6�   5*      5+      