using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using static System.Console;
using System.Threading.Tasks;
using ConsoleTableExt;

namespace DrinksInfo;

public class TableVisualizationEngine
{
    public static void ShowMenu<T>(List<T> tableData,[AllowNull]string tableName) where T:class{

        Clear();
        if(tableName ==null) tableName = "";

        WriteLine("\n\n");
        ConsoleTableBuilder
            .From(tableData)
            .WithColumn(tableName)
            .WithFormat(ConsoleTableBuilderFormat.Alternative)
            .ExportAndWriteLine(TableAligntment.Center);
        WriteLine("\n\n");

    }

}
