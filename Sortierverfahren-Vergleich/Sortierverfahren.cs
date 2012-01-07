/*
 * Created by SharpDevelop.
 * User: Tobias Markus
 * Date: 29.12.2011
 * Time: 15:53
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace Sortierverfahren_Vergleich
{
	/// <summary>
	/// Class for Sortierverfahren
	/// </summary>
	public static class Sortierverfahren
	{
		public static ulong counter = 0;
		
		/// <summary>
		/// Insertion Sort algorithm class
		/// </summary>
		public static class insertionSort {
			
			/// <summary>
			/// Insertion Sort function
			/// </summary>
			/// <param name="a">Array to sort</param>
			public static void sort(int[] a) {
				int i, j, wert;
				bool fertig;
				for(i = 1; i < a.Length - 1; i++) {
					wert = a[c(i)];
			        j = i - 1;
			        fertig = false;
			        do {
			        	if (a[c(j)] > wert) {
			        		a[c(j + 1)] = a[c(j)];
			                j--;
			                if (j < 0)
			                    fertig = true;
			        	}
			        	else {
			                fertig = true;
			        	}
			        }
			        while(!fertig);
			        a[c(j + 1)] = wert;
				}
			}
		}
		
		/// <summary>
		/// Quick Sort algorithm class
		/// </summary>
		public static class quickSort {
			/// <summary>
			/// Quick Sort function
			/// </summary>
			/// <param name="a">Array to sort</param>
			/// <param name="lowValue">First element of array</param>
			/// <param name="highValue">Last element of array</param>
			public static void sort(int[] a, int lowValue, int highValue) {
			    int i = lowValue, j = highValue, h;
			    int x = a[c((lowValue + highValue)/2)];
			    
			    do
			    {    
			    	while (a[c(i)] < x) i++;
			    	while (a[c(j)] > x) j--;
			        if (i <= j)
			        {
			        	h=a[c(i)]; a[c(i)]=a[c(j)]; a[c(j)]=h;
			            i++; j--;
			        }
			    } while (i<=j);
			
			    if (lowValue < j) Sortierverfahren.quickSort.sort(a, lowValue, j);
			    if (i < highValue) Sortierverfahren.quickSort.sort(a, i, highValue);
			}
		}
		
		/// <summary>
		/// Iterative quick sort algorithm class
		/// </summary>
		public static class quickSortIterativ {
			/// <summary>
			/// Iterative quick sort algorithm function
			/// </summary>
			/// <param name="a">The array to sort</param>
			public static void sort(int[] a) {
			 int L2, R2, PivotValue;
			 int links = 0;
			 int rechts = a.Length - 1;
			 Stack<int> stack = new Stack<int>();
			 
		     stack.Push(links);
		     stack.Push(rechts);	     // pushes Left, and then Right, on to a stack
		     while(stack.Count > 0) {
		     	 rechts = stack.Pop();
		         links = stack.Pop();		         // pops 2 values, storing them in Right and then Left
		         do {
		         	PivotValue = a[c((links + rechts) / 2)];
		             L2 = links;
		             R2 = rechts;
		             do {
		             	while(a[c(L2)] < PivotValue) { // scan left partition
		                     L2++;
		                }
		             	while(a[c(R2)] > PivotValue) { // scan right partition
			                     R2--;
			            }
		             	if (L2 <= R2) {
		             		if(L2 != R2)
		                         exch(ref a, L2, R2);  // swaps the data at L2 and R2
		                     L2 = L2 + 1;
		                     R2 = R2 - 1;
		             	}
		             }
		             while (L2 < R2);
		             if ((R2 - links) > (rechts - L2)) {// is left side piece larger?
		             
		             	if (links < R2) {
		                    stack.Push(links);
		                    stack.Push(R2);
		             	}
		             	
		                links = L2;
		             }
		             else {
		             	if(L2 < rechts) {// if left side isn't, right side is larger
		                     stack.Push(L2);
		                     stack.Push(rechts);
		             	}
		                rechts = R2;
		             }
		         } while(links < rechts);
		     }
			}
			
			/// <summary>
			/// Exchange to array values
			/// </summary>
			/// <param name="a">Array the values are from</param>
			/// <param name="firstIndex">First index</param>
			/// <param name="secondIndex">Second index</param>
			private static void exch(ref int[] a, int firstIndex, int secondIndex) {
				int temp = a[c(firstIndex)];
				a[c(firstIndex)] = a[c(secondIndex)];
				a[c(secondIndex)] = temp;
			}
		}
		
		/// <summary>
		/// Heap sort algorithm function
		/// </summary>
		public static class HeapSort {
		    private static int[] a;
		    private static int n;
			
		    /// <summary>
		    /// Sort array using Heap sort algorithm
		    /// (Initialization work)
		    /// </summary>
		    /// <param name="a0">The array to sort</param>
		    public static void sort(int[] a0)
		    {
		        a=a0;
		        n=a.Length;
		        heapsort();
		    }
			
		    /// <summary>
		    /// Sort array using Heap sort
		    /// </summary>
		    private static void heapsort()
		    {
		        buildheap();
		        while (n>1)
		        {
		            n--;
		            exchange(0, n);
		            downheap(0);
		        } 
		    }
			
		    /// <summary>
		    /// Build a heap for the array
		    /// </summary>
		    private static void buildheap()
		    {
		        for (int v=n/2-1; v>=0; v--)
		            downheap(v);
		    }
		
		    /// <summary>
		    /// Finds descandants of node v,
		    /// If v has descendants bigger than itself,
		    /// the nodes are being swapped.
		    /// </summary>
		    /// <param name="v"></param>
		    private static void downheap(int v)
		    {
		        int w=2*v+1;    // first descendant of v
		        while (w<n)
		        {
		            if (w+1<n)    // is there a second descendant?
		            	if (a[c(w+1)]>a[c(w)]) w++;
		            // w is the descendant of v with maximum label
		
		            if (a[c(v)]>=a[c(w)]) return;  // v has heap property
		            // otherwise
		            exchange(v, w);  // exchange labels of v and w
		            v=w;        // continue
		            w=2*v+1;
		        }
		    }
			
		    /// <summary>
		    /// Exchange two values in an array
		    /// </summary>
		    /// <param name="i">First index</param>
		    /// <param name="j">Second index</param>
		    private static void exchange(int i, int j)
		    {
		    	int t=a[c(i)];
		    	a[c(i)]=a[c(j)];
		    	a[c(j)]=t;
		    }
		}
		
		/// <summary>
		/// Merge sort algorithm class
		/// </summary>
		public static class MergeSort { 
		    private static int[] a, b;    // auxiliary array b           
			
		    /// <summary>
		    /// Initialization work
		    /// </summary>
		    /// <param name="a0">Array to sort</param>
		    public static void sort(int[] a0)
		    {
		        a=a0;
		        int n=a.Length;
		        // according to variant either/or:
		        b=new int[n];
		        mergesort(0, n-1);
		    }
		
		    /// <summary>
		    /// Actual merge sort algorithm (recursive)
		    /// </summary>
		    /// <param name="lo">low value</param>
		    /// <param name="hi">high value</param>
		    private static void mergesort(int lo, int hi)
		    {
		        if (lo<hi)
		        {
		            int m=(lo+hi)/2;
		            mergesort(lo, m);
		            mergesort(m+1, hi);
		            merge(lo, m, hi);
		        }
		    }
			
		    /// <summary>
		    /// Perform a merge sort
		    /// </summary>
		    /// <param name="lo">low value</param>
		    /// <param name="m">middle value</param>
		    /// <param name="hi">high value</param>
		    private static void merge(int lo, int m, int hi)
		    {
		        int i, j, k;

			    i=0; j=lo;
			    // copy first half of array a to auxiliary array b
			    while (j<=m)
			    	b[i++]=a[c(j++)];
			
			    i=0; k=lo;
			    // copy back next-greatest element at each time
			    while (k<j && j<=hi)
			    	if (b[i]<=a[c(j)])
			    		a[c(k++)]=b[i++];
			        else
			    		a[c(k++)]=a[j++];
			
			    // copy back remaining elements of first half (if any)
			    while (k<j)
			    	a[c(k++)]=b[i++];
			}
		}
		/// <summary>
		/// Function used to count array accesses
		/// </summary>
		/// <param name="k">Index that was accessed</param>
		/// <returns>Index that was accessed</returns>
		private static int c(int k)
		{
		    counter++;
		    return k;
		}
	}
	
	/// <summary>
	/// Management class to perform easy sorting
	/// </summary>
	public class Sortierer {
		
		private int[] array;
		private ulong counter = 0;
		private Verfahren verfahren;
		
		/// <summary>
		/// Enumeration of available algorithms
		/// </summary>
		public enum Verfahren {
			insertionSort, heapSort, quickSort, quickSortIterativ, mergeSort
		}
		
		/// <summary>
		/// Enumeration of array order
		/// </summary>
		public enum Order {
			Ascending, Descending, Random
		}
		
		/// <summary>
		/// Int array of available array sizes
		/// </summary>
		public static int[] Sizes = {
			1000, 10000, 100000
		};
		
		/// <summary>
		/// Constructor to perform new sort
		/// </summary>
		/// <param name="length">Length of array (one of 'Sizes')</param>
		/// <param name="ord">Order of array (one of 'Order')</param>
		/// <param name="verfahren">Which algorithm to use (one of 'Verfahren')</param>
		public Sortierer(int length, Order ord, Verfahren verfahren) {
			initializeArray(ord, length);
			this.verfahren = verfahren;
		}
		
		/// <summary>
		/// Performs the actual sort (calls the right 
		/// method inside the Sortierverfahren class)
		/// </summary>
		/// <returns>Current counter (this.counter)</returns>
		public ulong Sort() {
			switch(verfahren) {
				case Verfahren.insertionSort:
					Sortierverfahren.insertionSort.sort(this.array);
					break;

				case Verfahren.quickSort:
					Sortierverfahren.quickSort.sort(this.array, 0, this.array.Length - 1);
					break;
					
				case Verfahren.quickSortIterativ:
					Sortierverfahren.quickSortIterativ.sort(this.array);
					break;
					
				case Verfahren.heapSort:
					Sortierverfahren.HeapSort.sort(this.array);
					break;
				
				case Verfahren.mergeSort:
					Sortierverfahren.MergeSort.sort(this.array);
					break;

				default:
					return 0;
			}
			
			counter = Sortierverfahren.counter;
			Sortierverfahren.counter = 0;
			return counter;
		}
		
		/// <summary>
		/// Initializes an array with given size and order
		/// </summary>
		/// <param name="ord">One of [Ascending, Descending, Random]</param>
		/// <param name="length">One of [1000, 10000, 100000]</param>
		private void initializeArray(Order ord, int length) {
			array = new int[length];
			switch(ord) {
				case Order.Ascending:
					for(int i = 0; i < length; i++)
						array[i] = i;
					break;

				case Order.Descending:
					for(int i = length; i > 0; i--)
						array[length - i] = i;
					break;
				
				case Order.Random:
					Random rand = new Random();
					for(int i = 0; i < length; i++)
						array[i] = rand.Next(0, 999);
					break;
			}
		}
	}
}
