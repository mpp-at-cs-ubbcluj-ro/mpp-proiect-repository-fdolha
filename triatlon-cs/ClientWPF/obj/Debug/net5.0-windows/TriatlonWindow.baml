   M S B A M L   `   `   `  ����D  @ClientWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:ClientWPF	ClientWPF  5   	       ClientWPF.TriatlonWindow   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:ClientWPF     5   	     � Title$  TriatlonWindow��5   	   $	��450��6    $	��800��6-   .��5	      � .+�5
   
   �� -   5         ��Name$ RefereeName��5
      $��Nume Arbitru��6%   $
��Left=�6<   $��	30,30,0,0q�6W   $	��Top=�6j   $��18(�6�   $
��Bold"�6�   �� 5   
   -   5        $ RefereeRaceType��5      $��Proba��6)   $
��Left=�69   $��	30,59,0,0q�6T   $	��Top=�6g   �� 5   
   -   5         ��Name$ LogOutButton��5      $��LogOut��6'   $
��Left=�68   $��
715,49,0,0q�6S   $	��Top=�6g   6   �� 5   
   $
��Left=�6   $��1��6/   $��	30,90,0,0q�6:   
�������$	��Top=�6Y   $	��300��6q   �� 5   
   $��Participanti:��6   $
��Left=�6(   $��
30,112,0,0q�6C   $	��Top=�6W   �� 5   
   -   5        $ AddResultButton��5      $��Adauga Rezultat��6*   $
��Left=�6D   $��668,115,0,0q�6_   $	��Top=�6t   6�   �� 5   
   -   5        $ LeaderboardButton��5      $��Leaderboard��6,   $
��Left=�6B   $��588,115,0,0q�6]   $	��Top=�6r   6�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ ResultsDataGrid��5         AutoGenerateColumns$ False��6,   $��30,154,41,26q�6H    � 	Resources 5      =  7System.Windows.Controls.Primitives.DataGridColumnHeader �� 
TargetType �� Property �� Value��%z   '        �� 5      "  6   .�5      ��  � ��
 �����63   �� 5       � ��$
 Bold"�63   5      5      	   Columns	 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn
   Header$
 ID��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$	 100 6H   1  +System.Windows.Controls.DataGridBoundColumn   Binding 61   �� *Id+6H     5      $

 Nume��6%   $ * 6L    63   �� *Name+6L     5      $
 Total Puncte��6%   $	 200 6V    6;   �� *Points+6V   5      5      5      