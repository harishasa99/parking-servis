   M S B A M L   `   `   `  ����C  ?Hari_WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:Hari_WPF.FormeHari_WPF.Forme  5   	       Hari_WPF.Forme.wpfAdmin   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:Hari_WPF.Forme     5   	   -   5          � Title$  wpfAdmin��5   	   $	��650��6   $
��1000��6'   .��5      �  � ColumnDefinitions 5   
   �� 5      $
��1.5* �6   6.   �� 5      6!    � RowDefinitions 5   
   �� 5      $��4* �6   6*   �� 5      6   .+�5   
   &   System.Windows.Controls.DataGrid  -   5          Name$ dgEvidencija��5         RowBackground
 �����   AlternatingRowBackground
 ��ح�   HeadersVisibility$	 All=�6l      RowStyle 5      �� 5       �� 
TargetType)  #System.Windows.Controls.DataGridRow"  6   .�5      -   5        5      5        5    
   -   5        $ dgKorisnici��5    4   $��1��6   $��1��6#   
 �����
 ��ح�$	 All=�6�    5!      �� 5"      "  6   .�5$      -   5        5%      5(      � 5)   
   $��1��6    5*      �� 5+      6"   �� 5,      6"   �� 5-      6"   �� 5.      6"   �� 5/      6"    51      �� 52      6%   �� 53      6%   .+�55      �� -   5        $��Brisi Sve Parkirane��55      $	��120��6H   $
��Left=�6T   6q   �� 56      $��Brisi Sve korisnike��6   $��0��6W   $	��120��6g   $��Right=�6s   ��63   �� *BrisiKorisnike1+6�   �� 57      $��2��6f   ��6   �� 	 �� Path$	 TNovca�
 �� UpdateSourceTrigger$
 PropertyChanged=� �� Mode$ TwoWay=�6x   �� 58      $��Cena po sekundi��6   $��1��6.   6=   �� 59      -   5         ��Name$ txtCena��59      $��2��6'   $	��150��64   $
��Left=�6@   6^   �� 5;      $��Promotivna Cena��6   $��1��6.   $��1��6;   6M   �� 5<      -   5        $ txtCenaPopust��5<      $��2��6-   $��1��6:   $	��150��6J   $
��Left=�6V   6s   �� 5>      $��Broj Parkiranja za Promo��6   $��3��67   $��1��6D   6V   �� 5?      -	   5        $ txtPromoParking��5?      $��3��6/   $��1��6<   $	��150��6L   $��Right=�6X   6v   �� 5A      -
   5         ��Name$ btnOk��5A   /   $��Ok��6   $��4��6"   6T   �� 5B      -   5        $ 	btnOtkazi��5B   C   $��Otkazi��6   $��4��6&   $��1��63   6p   5C      5E      5F      