   M S B A M L   `   `   `  ����
  WPFApp    WPFApp.ReportStatistics   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      ��5      �� 5	   
     �� 
StartPoint$	  0,0&�6    �� EndPoint$	 1,1&�6/   .��5
      �  � Color$ #8A2BE2��6    � Offset$	 0.0Q�6+   � 5      $ LightSkyBlue��6   $	 1.0Q�60   5       � 	Resources 5       �� 
TargetType �� Property �� Value �� Triggers	 P� Property
 P� Value��%j  '��       �� 5   
   " ��6   .�5      ��  � ��$ Segoe UI)�6+   �� 5       � ��$ 14(�6)   �� 5       � ��$	 5,2q�6(   �� 5       � ��$ 5q�6'   �� 5       � ��
 �����6+   �� 5       � ��
 �����6,   �� 5       � ��$ 1q�60   �� 5       �@�Cursor$
 Hand��6'   �� 5       �@^�ToolTip$ Click to update profile��67    5      P� 5      	 �@J�IsMouseOver$

 True��61   . �5      ��  � ��
 ��ح�63   5      P� 5      	 � ��$

 True��6/   . �5      ��  � ��
 ��·�63   5      5!      5"      .��5#      �  � RowDefinitions 5$   
   �� 5%      $
��Auto �6   �� 5&      $��* �6   .+�5)   
   �� $��Report Statistics��6   $
��Bold"�6-   $��16(�6?   $��10q�6M   $��Center=�6Y   &   System.Windows.Controls.DataGrid  5*   
   -   5          Name$ dgReportStatistics��5*         AutoGenerateColumns$ False��6/      CanUserAddRows$ False��6K   $��1��6b   $��10q�6o      Columns 5+      0  *System.Windows.Controls.DataGridTextColumn  5,      ,  &System.Windows.Controls.DataGridColumn   Header$ 	Statistic��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6V   1  +System.Windows.Controls.DataGridBoundColumn   Binding 68   �� *	Statistic+6V     5-      $ Value��6%   $ * 6N    64   �� *Value+6N   5/      50      51      