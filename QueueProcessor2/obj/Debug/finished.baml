   M S B A M L   `   `   `  ����J  FQueueProcessor2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:QueueProcessor2.ServicesQueueProcessor2.Services  5   	   '    !QueueProcessor2.Services.finished   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   8local&clr-namespace:QueueProcessor2.Services     5   	     � Title$  finished��5   	   $	��250��6   $	��600��6'   .��5	      � .+�5
      �� $��Avg Turnaround Time:��6
   $
��Left=�6)   $��33��6D   $��27,17,-157,-50q�6P   $	��Top=�6h   $	��130��6�   �� 5      $��CPU Utilization��6
   $
��Left=�6$   $��33��6?   $��27,139,-121,-172q�6K   $	��Top=�6e   $��94��6}   �� 5      $��Average Wait Time
��6
   $
��Left=�60   $��33��6K   $��27,77,-157,-110q�6W   $	��Top=�6p   $	��130��6�   �� 5      -   5         ��Name$ turnAroundTime��5   
   $�� ��6"   $
��Left=�6-   $��33��6H   $��174,17,-124,0q�6T   $	��Top=�6k   $��58��6�   �� 5      -   5        $ waitTimeLabel��5   
   $�� ��6!   $
��Left=�6,   $��33��6G   $��143,77,-93,-15q�6S   $	��Top=�6k   $��58��6�   �� 5      -   5        $ cpuLabel��5   
   $�� ��6   $
��Left=�6'   $��33��6B   $��126,139,-76,-77q�6N   $	��Top=�6g   $��58��6   �� 5      -   5         ��Name$ okButton��5      $
��OKAY��6   $
��Left=�6C   $��59��6^   $��256,0,256,10q�6i   $��31��6   $��Bottom=�6�   �� 5      -   5         ��Name$ processWaitTime��5      $
��Left=�6'   $��315,46,-279,-85q�6B   $
��Wrap=�6[   $	��Top=�6o   $	��134��6�   $��72��6�   �� 5      -   5        $ processTurnaround��5      $��437,50,75,0q�6)   $
��Wrap=�6>   $	��Top=�6R   $	��134��6j   �� 5      $��	Wait Time��6
   $
��Left=�6   $��32��69   $��307,14,-299,0q�6E   $	��Top=�6\   $	��100��6t   �� 5      $��Turnaround Time��6
   $
��Left=�6$   $��36��6?   $��427,14,-419,0q�6K   $	��Top=�6b   $	��100��6z   5      5      