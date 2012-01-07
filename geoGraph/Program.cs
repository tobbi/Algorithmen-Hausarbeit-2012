/*
 * Created by SharpDevelop.
 * User: Tobbi
 * Date: 23.12.2011
 * Time: 02:00
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace pointDistance
{
	class Program
	{
		/// <summary>
		/// Einstiegspunkt des Programms
		/// </summary>
		public static void Main()
		{
			// Wir legen ein neues Array an und testen unser Programm!
			Point[] pArray = new Point[10];
			pArray[0] = new Point(8, 24);
			pArray[1] = new Point(9, 12);
			pArray[2] = new Point(3, 7);
			pArray[3] = new Point(13, 30);
			pArray[4] = new Point(12, 9);
			pArray[5] = new Point(4, 8);
			pArray[6] = new Point(8, 28);
			pArray[7] = new Point(10, 11);
			pArray[8] = new Point(28, 12);
			pArray[9] = new Point(6, 18);
			
			// Erstellung eines neuen Geographen
			GeoGraph g = new GeoGraph(pArray);
			
			// Erstellung eines minimalen Spannbaums
			MinimumSpanningTree tree = new MinimumSpanningTree(g);
			
			// Damit das Fenster offen bleibt, müssen wir hier Console.ReadKey() ausführen:
			Console.ReadKey();
		}
	}
}

/// <summary>
/// Klasse, die einen Punkt (x|y) in einem Koordinatensystem darstellt
/// </summary>
public class Point
{
	/// <summary>
	/// x-Komponente des Punktes
	/// </summary>
	public int x;
	
	/// <summary>
	/// y-Komponente des Punktes
	/// </summary>
	public int y;

	/// <summary>
	/// Konstruktor 
	/// </summary>
	/// <param name="x">x-Komponente des Punktes</param>
	/// <param name="y">y-Komponente des Punktes</param>
	public Point(int x, int y) {
		this.x = x;
		this.y = y;
	}
	
	/// <summary>
	/// Zur Berechnung der Distanz wird der Satz
	/// des Pythagoras verwendet. Man kann die
	/// Längen- und Breitenkomponente jeweils
	/// zur Berechnung verwenden.
	/// Satz des Pythagoras:  a² + b² = c² ->
	/// c = sqrt(a² + b²)
	
	/// Durch die Verwendung des Betrages wird eine
	/// eventuelle negative Entfernung positiv! (Math.Abs)
	/// </summary>
	/// <param name="q">Punkt zu dem die Entfernung gemessen werden soll</param>
	/// <returns>Den Abstand des Punktes</returns>
	public double distanceTo(Point q) {
		//Absolutbetrag der beiden X-Werte:
		int sideA = Math.Abs(this.x - q.x);
		
		//Absolutbetrag der beiden Y-Werte:
		int sideB = Math.Abs(this.y - q.y);
		
		//Anwendung des Satz des Pythagoras:
		double distance = Math.Sqrt(Math.Pow(sideA, 2) + Math.Pow(sideB, 2));
		
		return distance;
	}
}

/// <summary>
/// WeightedGraph Interface-Beschreibung
/// </summary>
public interface WeightedGraph 
{
    int size();                    // gibt die Anzahl der Knoten des Graphen zurück
    bool isDirected();             // gibt true zurück, wenn der Graph gerichtet ist
    double noEdge();               // gibt das Gewicht nicht vorhandener Kanten zurück
    void setWeight(int i, int j, double x);   // belegt Kante (i, j) mit Gewicht x 
    double getWeight(int i, int j);       // gibt das Gewicht von Kante (i, j) zurück
    void deleteEdge(int i, int j);        // löscht die Kante (i, j)
    bool isEdge(int i, int j);     // gibt true zurück, wenn (i, j) eine Kante ist
}