   M S B A M L   `   `   `  ����B  >Napelem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:NapelemNapelem  5   	       Napelem.stockKeeper   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:Napelem     5   	     � Title$  Stock Keeper��5   	   $	��450��6   $	��800��6+    � WindowStyle$
 None=�5	   	    � WindowStartupLocation$ CenterScreen=�5
   	    � AllowsTransparency$
 True��5   	   
������ .��5      �� ���	#FF1B1919$��1q�6$    �� CornerRadius$ 30��68   ._�5   
   � $��Stretch=�6   $��Stretch=�6-   .+�5      �� ���	#FF1B1919���	#FF1B1919.@�5      �� $��Projects��6   ���	#FF1B1919
��� ������	#FF1B1919.��5      � ���	#FF1B1919.+�5      �� $��Warhouse worker page��6    $
��Left=�6?   $��13,7,0,0q�6Z   $	��Top=�6l   
��� ���$��20(�6�   $
��Bold"�6�   �� 5      -   5         ��Name$ backBtn��5   !      ExitButtonTemplate#��[  62   $
��Back��6^   $��51��6m   $��16(�6x   $
��Bold"�6�   6�   �� 5      $��Project��6    $
��Left=�62   $��
521,19,0,0q�6M   $	��Top=�6a   $��16(�6y   $
��Bold"�6�   
�������6�   �� 5      -   5         ��Name$ projectcmbbx��5   #   $
��Left=�69   $��
605,26,0,0q�6T   $	��Top=�6h   $	��120��6�   �� 5      $��Assets and position��6    $
��Left=�6>   $��	45,86,0,0q�6Y   $	��Top=�6l   $��16(�6�   $
��Bold"�6�   
�������6�   � 5      $	��750��6   $	��290��6+   .+�5      &   System.Windows.Controls.DataGrid  -   5        $��ProjectDataGrid��5   '   5      �� 5      -   5          RoundeButtonTemplate#��[ 5   !   $��Refresh��5      $
��Left=�6#   $��	21,40,0,0q�5      $	��Top=�5      $��65��5      $��20��6   $
��Bold"�5      
���   �
��� ��� J� RenderTransformOrigin$ 0.67,-0.759&�5    8   �� 5!      -   5        $ ChangeStatus��5"      #��[ 5!   !   $��Change status��5#      $
��Left=�5$      $��625,390,0,0q�5%      $	��Top=�5&      $	��135��5'      $��20��6)   $
��Bold"�5(      
���   �
��� ���5*   1   5+      5,      �� 5-      $��Assett output��6   ���	#FF1B1919
��� ������	#FF1B1919.��5.      � ���	#FF1B1919.+�5/      �� $��Project details��6    $
��Left=�6:   $��11,5,0,0q�6U   $	��Top=�6g   
��� ���$��20(�6�   $
��Bold"�6�   �� 50      $��Project��6    $
��Left=�62   $��
521,19,0,0q�6M   $	��Top=�6a   $��16(�6y   $
��Bold"�6�   
�������6�   �� 51      -   5        $��FilterProject��51   #   $
��Left=�68   $��
605,26,0,0q�6S   $	��Top=�6g   $	��120��6   6�   �� 52      $�� ��6    $
��Left=�6+   $��
24,125,0,0q�6F   $	��Top=�6Z   
�������$��16(�6�   $
��Bold"�6�   � 53      $	��300��6   $��24,76,-231,44q�6,   $	��700��6C   .+�54        -   5        $��WorkerDataGrid��54   '   $��-154,-12,0,-12q�6h   $
��Left=�6�   $	��751��6�   $ 	0.428,0.5&�6�   55      56      57      58      5:      5;      5<      