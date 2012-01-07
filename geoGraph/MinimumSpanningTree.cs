/*
 * Created by SharpDevelop.
 * User: Tobbi
 * Date: 28.12.2011
 * Time: 22:42
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
 
#region Using directives
using System;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
#endregion

class MinimumSpanningTree: WeightedGraph {
	
	/// <summary>
	/// Quell-Graph
	/// </summary>
	private WeightedGraph sourceGraph = null;
	/// <summary>
	/// Attribut, das das Gewicht des Graphen hält
	/// </summary>
	private double weight = 0;
	
	/// <summary>
	/// Liste von Knoten, die bereits zum minimalen Spannbaum gehören
	/// </summary>
	private ArrayList foundNodes = new ArrayList();
	
	/// <summary>
	/// WindowsForms-Form, die später die Knoten anzeigt
	/// </summary>
	private Form form;

	/// <summary>
	/// Konstruktor für minimalen Spannbaum
	/// </summary>
	/// <param name="g">GeoGraph aus dem ein 
	/// minimaler Spannbaum erstellt werden soll</param>
	public MinimumSpanningTree(GeoGraph g) {
		this.sourceGraph = g;
		this.form = new Form();
		this.form.Show();
		
		computeMinimumSpanningTree();
	}
	
	/// <summary>
	/// Berechnet einen minimalen Spannbaum
	/// </summary>
	private void computeMinimumSpanningTree() {
		// Methode:
		// 1. Beliebigen Knoten (hier: 1. Knoten) hinzufügen
		// 2. Das dem Baum am nächsten liegende Ziel berechnen
		//    und dem Ziel-Array hinzufügen!
		// 3. Gewicht um Länge der Strecke erhöhen!
		int nearestSource, nearestTarget;
		double nearestWeight;
		
		//Zeichne die Punkte ein:
		Graphics gfx = form.CreateGraphics();
		((GeoGraph)(sourceGraph)).drawPoints(gfx);
		
		for(int i = 0; i < sourceGraph.size(); i++) {
			// Da noch keine Knoten im Array sind, fügen wir
			// einfach den ersten Knoten zum Array hinzu und
			// fahren von dort fort:
			if(i == 0) {
				foundNodes.Add(0);
				continue;
			}
			
			nearestSource = this.getNearestSource();
			nearestTarget = this.getNearestTarget(nearestSource);
			nearestWeight = this.getWeight(nearestSource, nearestTarget);
			foundNodes.Add(nearestTarget);
			weight += nearestWeight;
			
			//Zeichne neue Kante ein
			((GeoGraph)(sourceGraph)).drawEdge(gfx, nearestSource, nearestTarget);
		}
	}
	
	/// <summary>
	/// Berechnet den einem bestimmten Knoten am
	/// nächsten liegenden Knoten, der nicht bereits
	/// Teil des Baums ist
	/// </summary>
	/// <param name="index">Index des Start-Knotens</param>
	/// <returns>Index des Ziel-Knotens</returns>
	public int getNearestTarget(int index) {
		// Distanz des am nächsten liegenden Nachbarn
		// (mit Unendlich initialisiert, da jeder Nachbar
		// zunächst kleiner sein soll!)
		double minDistance = double.PositiveInfinity;
		// Index des am nächsten liegenden Nachbarn:
		int nearestNeighbour = -1;
		// Aktuelles Gewicht:
		double currentWeight = 0;

		for(int i = 0; i < this.size(); i++) {
			// Verbindungen mit dem gleichen Knoten werden verhindert (wäre 0)!
			if(i == index)
				continue;

			//Nachbar soll auch nicht bereits Teil des minimalen Spannbaums sein!
			if(foundNodes.Contains(i))
				continue;
			
			currentWeight = this.getWeight(index, i);
			
			if(currentWeight < minDistance) {
				minDistance = currentWeight;
				nearestNeighbour = i;
			}
		}

		return nearestNeighbour;
	}
	
	/// <summary>
	/// Diese Methode überprüft alle bereits zum
	/// Spannbaum gehörenden Knoten und findet jeweils
	/// den Knoten heraus, der dem Zielknoten am nächsten 
	/// liegt ("Quellknoten") und gibt den Index zurück
	/// </summary>
	/// <returns>Index des Knotens der dem Zielknoten am
	/// nächsten liegt</returns>
	public int getNearestSource() {
		int current = 0, target = 0, nearestTarget = 0;
		double weight, nearestWeight = double.PositiveInfinity;

		for(int i = 0; i < foundNodes.Count; i++) {
			// Aktueller Knoten:
			current = Convert.ToInt32(foundNodes[i]);
			// Am nächsten liegender Nachbarknoten:
			target = this.getNearestTarget(current);
			// Länge der Strecke ("Gewicht") der Verbindung:
			weight = this.getWeight(current, target);
			
			if(weight < nearestWeight) {
				nearestTarget = current;
				nearestWeight = weight;
			}
		};
		
		return nearestTarget;
	}
	
	/// <summary>
	/// Gibt die Größe des Baums zurück
	/// </summary>
	/// <returns>Anzahl der Knoten des Baums</returns>
	public int size() {
		return this.sourceGraph.size();
	}
	
	/// <summary>
	/// Gibt an, ob der Graph gerichtet oder ungerichtet ist.
	/// </summary>
	/// <returns>isDirected() of the source graph</returns>
	public bool isDirected() {
		return this.sourceGraph.isDirected();
	}
	
	/// <summary>
	/// Gibt das Gewicht von 'keiner Kante' zurück
	/// </summary>
	/// <returns>noEdge of the source graph</returns>
	public double noEdge() {
		return this.sourceGraph.noEdge();
	}
	
	/// <summary>
	/// Berechnet das Gewicht einer Kante
	/// </summary>
	/// <param name="i">Index des Startknotens der Kante</param>
	/// <param name="j">Index des Zielknotens der Kante</param>
	/// <returns>Gewicht der Kante</returns>
	public double getWeight(int i, int j) {
		return this.sourceGraph.getWeight(i, j);
	}
	
	/// <summary>
	/// Gibt das Gewicht des Baumes zurück
	/// </summary>
	/// <returns>weight of the tree</returns>
	public double getWeight() {
		return this.weight;
	}
	
	/// <summary>
	/// Leere Schnittstellen-Implementation
	/// </summary>
	/// <param name="i"></param>
	/// <param name="j"></param>
	/// <param name="x"></param>
	public void setWeight(int i, int j, double x) {
	}
	
	/// <summary>
	/// Leere Schnittstellen-Implementation
	/// </summary>
	/// <param name="i"></param>
	/// <param name="j"></param>
	public void deleteEdge(int i, int j) {
	}
	
	/// <summary>
	/// Gibt an, ob eine bestimmte Kante von Index i nach Index j existiert
	/// (Funktion isEdge des Quellgraphen wird aufgerufen)
	/// </summary>
	/// <param name="i">Index des Startknotens</param>
	/// <param name="j">Index des Zielknotens</param>
	/// <returns>true wenn eine Kante existiert</returns>
	public bool isEdge(int i, int j) {
		return this.sourceGraph.isEdge(i, j);
	}
}