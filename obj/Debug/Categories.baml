   M S B A M L   `   `   `  ����?  ;dash, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:dashdash  5   	       dash.Categories   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   $localclr-namespace:dash     5   	     � Title$  
Categories��5   	   $	��700��6   $
��1200��6)   ��5      �� 5   
    �� Color$ #F8F8F8��6    � 	Resources 5         ManropeFont��%G   &	        *�5   
   )%pack://application:,,,/fonts/#Manrope)�6P   5      .��5      � -   5         �Name$ 
windowGrid��5      $��Center=�6   $
��1200��6<   $��0,0,0,-6q�6I    � RowDefinitions 5   
   �� 5      $
��249* �6   �� 5      $
��505* �6    � ColumnDefinitions 5   
   .+�5   
   �� $
��Left=�6   $	��Top=�6,   $	��400��5    	   $��75��6   $��1,10,0,0q�6!    �� CornerRadius$ 20��5!   	   
�������$��2q�6/   ._�5"      �� -   5         ��Name$ search��5"      $��UniformToFill=�6$   $��/images/serach.png��5#      $��198,8,-746,-103q�6(   5$      �� 5%   
   -   5         ��Name$ sideBar��5%      
�������$
��Left=�67   $	��670��6R   $	��Top=�6_   $	��239��6w   $��0,0,1,0q�6�   $��2��6�   ��5&      �� 5'      $ #F1F1FA��6"   ._�5)      �� $��bookTook��6   $��32��6'   $��	10,33,0,0q�63   $	��Top=�6F   $��18(�5*      #��[  5+      ���	#FF101720	 J� RenderTransformOrigin$	 0.537,1.428&�6R   $
��Left=�6v   $��94��6�   5,      �� 56   
   $��0,125,974,10q�6   $��Right=�6+   $	��196��6G   .+�57      �� $
��Left=�6   $	��Top=�63   $��10(�6K   �� 58      -   5        .�58   3   Home69   59      �� 5:      $
��Left=�6   $	��Top=�63   $��10(�6K   �� 5;      -   5        .�5;   8   	Dashboard6C   5<      �� 5=      $
��Left=�6   $	��Top=�63   $��10(�6K   �� 5>      .�6   
Categories6(   5?      5@      �� 5A   
   -   5        $��	All Books��5A      $
��Left=�6%   $	��Top=�6@   $	��100��6X   $��894,203,0,0q�6d   6y   �� 5B   
   -   5        $��Trending��5B      $
��Left=�6$   $��20��6?   $��700,203,0,0q�6K   $	��Top=�6`   $��90��6x   6�   �� 5C   
   -   5        $��All Times Popular��5C      $��Center=�6-   $��20��6J   $��	0,203,0,0q�6V   $	��Top=�6i   $	��127��6�   6�   �� 5Y   
   -	   5        
 ��Name$
 bookListBox��5Y      $��226,5,27,84q�6,   $��1��6A   ��5Z      �� 5[      .R�5\      �  � Orientation$ 
Horizontal=�6    5]      ��5_      �� 5`      .R�5a      �� 
�������$��1q�64   $��10q�6H    �� Padding$ 10q�6T   $	��150��6a   $��60��6m   $ 5��6y   ���#F9F9F9._�5b      �� $
��Wrap=�5c       $��Center=�5d       $��Center=�5e       $��SemiBold"�5f       ��5b   $   �� 5f       5g      5h      5j      �� 5k   
   $
��Auto=�6   $
��Auto=�6:   $��264,10,27,20q�6_   $��1��6u   5n      5p      