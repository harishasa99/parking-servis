   M S B A M L   `   `   `  ����C  ?Hari_WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:Hari_WPF.FormeHari_WPF.Forme  5   	   $    Hari_WPF.Forme.wpfRegistracija   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:Hari_WPF.Forme     5   	   -   5          � Title$%  Registracija Korisnika Parkinga��5   	   $	��450��61   $	��800��6>   .��5      �  � RowDefinitions 5   
   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6   �� 5      6   .+�5   
   �� $��Unesite Korisnicko ime:��6   64   �� 5   
   $��1��6h   ��6   �� *
vo.kor_ime+ �� Mode$ OneWayToSource=� �� UpdateSourceTrigger$ PropertyChanged=�6w   �� 5   
   $��2��6   $��Unesite sifru��6   68   G� 5   
   -   5         G�Name$ txtSifra��5      $��3��6(   67   �� 5!   
   $��4��6   $��Unesite vlasnika��6   6;   �� 5"   
   $��5��6h   ��6   �� *
vo.vlasnik+$ OneWayToSource=�$ PropertyChanged=�6w   �� 5$   
   $��6��6   $��Unesite broj tablica��6   6?   �� 5%   
   -   5         ��Name$ txtTest��5%      $��7��6|   ��6#   �� *vo.br_tablica+$ OneWayToSource=�$ PropertyChanged=�6�   �� 5'   
   $��8��6   $��Odaberite tip vozila��6   6?   �� 5(   
   -   5         ��Name$ cmbTipVozila��5(      $��9��6�    �� Text 6)   �� *vo.tip_vozila+$ OneWayToSource=�$ PropertyChanged=�6�   �� 5*   
   -   5         ��Name$ btnOk��5*      $��11��6    $
��Left=�6.   $��Ok��6I   $	��150��6V   $��5 0 0 0q�6b   6�   �� 5+   
   -   5        $ 	btnOtkazi��5+      $��11��6$   $��Right=�62   $��Otkazi��6N   $	��150��6_   $��0 0 5 0q�6k   6�   5-      5.      