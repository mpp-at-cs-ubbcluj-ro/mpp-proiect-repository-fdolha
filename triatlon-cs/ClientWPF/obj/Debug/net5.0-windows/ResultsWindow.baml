   M S B A M L   `   `   `  ����D  @ClientWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:ClientWPF	ClientWPF  5   	       ClientWPF.ResultsWindow   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:ClientWPF     5   	     � Title$  ResultsWindow��5   	   $	��450��6   $	��600��6,   .��5	      � .+�5
   
   �� -   5         ��Name$ ResultsTitle��5
      $��Label��6&   $
��Left=�66   $��	10,10,0,0q�6Q   $	��Top=�6d   $��18(�6|   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ ResultsDataGrid��5         AutoGenerateColumns$ False��6,   $��0,60,0,0q�6H    � 	Resources 5      =  7System.Windows.Controls.Primitives.DataGridColumnHeader �� 
TargetType �� Property �� Value��%z   '        �� 5      "  6   .�5      ��  � ��
 �����63   �� 5       � ��$
 Bold"�63   5      5         Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn	   Header$	 ID��6%   
   Width5  /System.Windows.Controls.DataGridLengthConverter$	
 100 6H   1  +System.Windows.Controls.DataGridBoundColumn   Binding 61   �� *Id+6H     5      $
	 Nume��6%   $
 * 6L    63   �� *Name+6L     5      $	 Puncte��6%   $	
 200 6P    65   �� *Points+6P   5      5      5      