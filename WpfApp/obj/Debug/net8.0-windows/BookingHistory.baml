   M S B A M L   `   `   `  ����A  =WPFApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:WPFAppWPFApp  5          WPFApp.BookingHistory   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      &localclr-namespace:WPFApp     5      ��5	      �� 5
   
     �� 
StartPoint$	  0,0&�6    �� EndPoint$	 1,1&�6/   .��5      �  � Color$ #8A2BE2��6    � Offset$	 0.0Q�6+   � 5      $ LightSkyBlue��6   $	 1.0Q�60   5       � 	Resources 5       �� 
TargetType �� Property �� Value �� Triggers	 P� Property
 P� Value��%j  '��       �� 5   
   " ��6   .�5      ��  � ��$ Segoe UI)�6+   �� 5       � ��$ 14(�6)   �� 5       � ��$	 5,2q�6(   �� 5       � ��$ 5q�6'   �� 5       � ��
 �����6+   �� 5       � ��
 �����6,   �� 5       � ��$ 1q�60   �� 5       �@�Cursor$
 Hand��6'   �� 5       �@^�ToolTip$ Click to update profile��67    5      P� 5      	 �@J�IsMouseOver$

 True��61   . �5      ��  � ��
 ��ح�63   5      P� 5      	 � ��$

 True��6/   . �5      ��  � ��
 ��·�63   5       5"      5#      .��5$      �  � RowDefinitions 5%   
   �� 5&      $
��Auto �6   �� 5'      $��* �6   �� 5(      $
��Auto �6   .+�5+   
   &   System.Windows.Controls.DataGrid  -   5          Name$ dgBookingHistory��5+         AutoGenerateColumns$ False��6-   $��1��6I      
IsReadOnly$
 True��6V      CanUserResizeColumns$ False��6h      CanUserReorderColumns$ False��6�      SelectionMode$ Single=�6�      CanUserAddRows$ False��6�      CanUserDeleteRows$ False��6�   $��10,21,10,21q�6�      Columns 5,      0  *System.Windows.Controls.DataGridTextColumn  5-      ,  &System.Windows.Controls.DataGridColumn   Header$ BookingReservationId��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ SizeToHeader 6m   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6C   �� *BookingReservationId+6m     5.      $ RoomId��6%   $ SizeToHeader 6Q    65   �� *RoomId+6Q     5/      $ 
RoomNumber��6%   $ SizeToHeader 6X    69   �� *
RoomNumber+6X     50      $ BookingDate��6%   $ SizeToHeader 6Z    6:   �� *BookingDate+6Z     51      $ 	StartDate��6%   $ SizeToHeader 6V    68   �� *	StartDate+6V     52      $ EndDate��6%   $ SizeToHeader 6R    66   �� *	EndDate+6R     53      $ ActualPrice��6%   $ SizeToHeader 6Z    6:   �� *ActualPrice+6Z     54      $ 
TotalPrice��6%   $ SizeToHeader 6X    69   �� *
TotalPrice+6X   56      �� 58   
    �� Orientation$ 
Horizontal=�6   $��Right=�6.   $��10q�6J   $��2��6V   .+�59      �� -   5        $	��Add��59      $��5q�68   �� 5:      -   5        $��Delete��5:      $��5q�6>   5;      5<      5=      