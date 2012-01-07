using System.Drawing;

class GeoGraph: WeightedGraph {
	
	/// <summary>
	/// Konstruktor, der einen gewichteten Graphen erstellt.
	/// Dazu wird zunächst das angegebene Array in ein privates
	/// Attribut übernommen.
	/// </summary>
	/// <param name="point">Array, das einzelne Punkte enthält</param>
	public GeoGraph(Point[] point) {
		this.points = new Point[point.Length];
		for(int i = 0; i < point.Length; i++) {
			this.points[i] = point[i];
		}
	}
	
	/// <summary>
	/// Array von Koordinaten, mit denen gearbeitet wird
	/// </summary>
	private Point[] points;
	
	/// <summary>
	/// Größe des Baumes
	/// </summary>
	/// <returns>Anzahl der Knoten des Baumes</returns>
	public int size() {
		return this.points.Length;
	}
	
	/// <summary>
	/// Gibt an, ob der Graph gerichtet ist. Da er laut Aufgabenstellung
	/// ungerichtet ist, geben wir 'false' zurück.
	/// </summary>
	/// <returns>'false', laut Aufgabenstellung</returns>
	public bool isDirected() {
		return false;
	}
	
	/// <summary>
	/// Gibt das Gewicht 'keiner Kante' zurück
	/// </summary>
	/// <returns>- unendlich</returns>
	public double noEdge() {
		return double.NegativeInfinity;
	}
	
	/// <summary>
	/// Leere Schnittstellen-Implementation
	/// </summary>
	/// <param name="i"></param>
	/// <param name="j"></param>
	/// <param name="x"></param>
	public void setWeight(int i, int j, double x) {
		// Nicht implementiert!
	}
	
	/// <summary>
	/// Berechnet die Distanz von einem Knoten zu einem anderen
	/// </summary>
	/// <param name="i">Startknoten</param>
	/// <param name="j">Zielknoten</param>
	/// <returns>Entfernung von Knoten i zu Knoten j</returns>
	public double getWeight(int i, int j) {
		return this.points[i].distanceTo(this.points[j]);
	}
	
	/// <summary>
	/// Leere Schnittstellen-Implementation
	/// </summary>
	/// <param name="i"></param>
	/// <param name="j"></param>
	public void deleteEdge(int i, int j) {
	}
	
	/// <summary>
	/// Gibt 'true' zurück, wenn von i nach j eine Kante besteht.
	/// Da theoretisch von jedem Punkt zu jedem anderen eine Kante
	/// bestehen könnte, geben wir hier 'true' zurück.
	/// </summary>
	/// <param name="i">Startknoten</param>
	/// <param name="j">Zielknoten</param>
	/// <returns></returns>
	public bool isEdge(int i, int j) {
		return true;
	}
	
	/// <summary>
	/// Skalierungsfaktor, der beim Zeichnen verwendet wird
	/// </summary>
	private const int SCALE_FACTOR = 8;
	
	/// <summary>
	/// Malt die Punkte auf dem Bildschirm auf
	/// und beschriftet sie mit den Koordinaten
	/// </summary>
	/// <param name="gr">Graphics-Objekt, auf dem gezeichnet wird</param>
	public void drawPoints(Graphics gr) {
		foreach(Point point in this.points) {
			gr.DrawRectangle(Pens.Black,
			                 point.x*SCALE_FACTOR, 
			                 point.y*SCALE_FACTOR,
			                 .5f, 
			                 .5f);
			
			//Punkte beschriften: 
			gr.DrawString("(" + point.x + "|" + point.y + ")",
			              new Font("Arial.ttf", 8, FontStyle.Bold),
			              Brushes.Black,
			              point.x*SCALE_FACTOR,
			              point.y*SCALE_FACTOR);
		}
	}
	
	/// <summary>
	/// Malt eine Kante auf dem Bildschirm auf
	/// </summary>
	/// <param name="gr">Graphics-Objekt, auf dem gezeichnet wird</param>
	/// <param name="i">Startknoten-Index</param>
	/// <param name="j">Endknoten-Index</param>
	public void drawEdge(Graphics gr, int i, int j) {
		gr.DrawLine(Pens.Black,
					this.points[i].x*SCALE_FACTOR,
		            this.points[i].y*SCALE_FACTOR,
		            this.points[j].x*SCALE_FACTOR,
		            this.points[j].y*SCALE_FACTOR);
	}
}