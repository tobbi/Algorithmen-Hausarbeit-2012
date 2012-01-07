/*
 * Created by SharpDevelop.
 * User: Tobias Markus
 * Date: 29.12.2011
 * Time: 01:35
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;

namespace Sortierverfahren_Vergleich
{
	class Program
	{	
		/// <summary>
		/// Creates a valid HTML string, opens an
		/// output stream, writes the HTML string
		/// to a file and closes the output stream;
		/// </summary>
		public static void Main()
		{
			string HTMLstring = 
				"<!DOCTYPE html>" +
				"<html>" +
					"<head>" +
						"<title>Sortierverfahren-Vergleich</title>" +
						"<meta http-equiv='Content-Type' content='text/html; charset=utf-8'>" +
					"</head>" +
					"<body>" +
						"<h1>Anzahl der Array-Operationen</h1>" +
						String.Format("<table>{0}{1}</table>",
				              tableHead(), tableBody()) +
					"</body>" +
				"</html>";

			StreamWriter writer = new StreamWriter("output.html");
			writer.Write(HTMLstring);
			writer.Close();
		}
		
		/// <summary>
		/// Creates a table head HTML string.
		/// Adds 'Verfahren' and all numbers
		/// to the table head
		/// </summary>
		/// <returns>string consisting of the table head</returns>
		private static string tableHead() {
			string result = "<thead><tr>";
			result += "<td>Verfahren</td>";
			foreach(int size in Sortierer.Sizes)
			{
				result += String.Format("<td>{0} Zahlen</td>", size);
			}
			result += "</tr></thead>";
			
			return result;
		}
		
		/// <summary>
		/// Writes a table body construct, including
		/// one cell including the way of sorting and 
		/// the array order
		/// </summary>
		/// <returns>String consisting of the table body</returns>
		private static string tableBody() {
			string result = "<tbody>";
			foreach(Sortierer.Verfahren verfahren in 
			        Sortierer.Verfahren.GetValues(typeof(Sortierer.Verfahren)))
				foreach(Sortierer.Order order in
				        Sortierer.Order.GetValues(typeof(Sortierer.Order)))
			{
				result += "<tr>"; 
				result += String.Format("<td>{0} ({1})</td>", verfahren, order);
				
				foreach(int size in Sortierer.Sizes)
				{
					result += String.Format("<td>{0}</td>", new Sortierer(size, order, verfahren).Sort());
				}
				
				result += "</tr>";
			}
			result += "</tbody>";
			return result;
		}
	}
}