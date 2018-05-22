using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Fraction
{
	public static string fractionKey = "Fraction";
	public static string numeratorKey = "Numerator";
	public static string denominatorKey = "Denominator";

	public int numerator;
	public int denominator;

	public Fraction(int numerator, int denominator) {
		this.numerator = numerator;
		this.denominator = denominator;
		DoPiMagic();
	
	}

//	public void SetFraction(int n, int d){
//		numerator = n;
//		denominator = d;
//	}

	public static Fraction GetFractionFromFloat(float x, float error=.001f){
		int n = Mathf.FloorToInt(x);
		x -= n;
		if (x < error){
			return new Fraction(n,1); 
		} else if (1 - error < x){
			// The lower fraction is 0/1
			return new Fraction(n+1, 1);
		} 

		int lower_n = 0;
		int lower_d = 1;
		// The upper fraction is 1/1
		int upper_n = 1;
		int upper_d = 1;
		while (true){ // what could go rong?
			// The middle fraction is (lower_n + upper_n) / (lower_d + upper_d)
			int middle_n = lower_n + upper_n;
			int middle_d = lower_d + upper_d;
			// If x + error < middle
			if (middle_d * (x + error) < middle_n){
				// middle is our new upper
				upper_n = middle_n;
				upper_d = middle_d;
				// Else If middle < x - error
			} else if (middle_n < (x - error) * middle_d) {
				// middle is our new lower
				lower_n = middle_n;
				lower_d = middle_d;
				// Else middle is our best fraction
			} else {
				return new Fraction(n * middle_d + middle_n, middle_d);
			}
		}
		return new Fraction(-101,1); //lol
	}

	public Fraction(float numerator, float denominator) {
		this.numerator = (int)numerator;
		this.denominator = (int)denominator;
		DoPiMagic();
	}

//	public bool IsFactorOf(Fraction fracB){
//		List<int> denominatorFactors = MathUtils.GetFactors(fracB.denominator);
//		if (denominatorFactors.Contains(denominator)){ // The denominator of 
//
//		}
//		Fraction fracA = new Fraction(numerator,denominator);
//
//
//
//
////		if (f.denominator != 1){
////			f = Fraction.Multiply(new Fraction(f.denominator,
////		}
//	}
	
	void DoPiMagic() {
		// MORE PI MAGIC
		if(this.denominator == 1000) {
			float num = (float)numerator / (float)denominator;
			float factorOf = (Mathf.PI / 8);
			
			//// commented Debug.Log("----------");
			int times = (int)Mathf.Round((float)num / factorOf);
			//// commented Debug.Log(times);
			float off = Mathf.Abs(num - factorOf * times);
			//// commented Debug.Log(off);
			if(off < 0.01f)
			{
				this.numerator = (int)Mathf.Round(factorOf * times * 1000.0f);
				//// commented Debug.Log("should = PI / 16 * " + times);
			}
		}
	}
	
	public override bool Equals (object obj)
	{
		if(Object.ReferenceEquals(null, obj)) { return false; }
		if(Object.ReferenceEquals(this, obj)) { return true; }
		if (this.GetType() != obj.GetType()) { return false; }
		Fraction f = (Fraction)obj;
		return f.numerator == this.numerator && f.denominator == this.denominator;
	}
	
	public override int GetHashCode ()
	{
		int hash = 27;
		hash = (13 * hash) + this.numerator.GetHashCode();
		hash = (13 * hash) + this.denominator.GetHashCode();
		return hash;
	}
	
	public float GetAsPercent() {
		return 100.0f * (float)numerator / (float)denominator;
	}

	public Fraction GetReciprocal() {
		return new Fraction(denominator, numerator);
	}
	
	public float GetAsFloat() {
		return (float)numerator / (float)denominator;	
	}

	public int GetAsInt() {
		return Mathf.RoundToInt((float)numerator / (float)denominator);	
	}
	
	public Fraction Negative() {
		return new Fraction(-numerator, denominator);	
	}

	public bool IsMultipleOf(Fraction f){
		return Fraction.Multiply(this,f.GetReciprocal()).denominator == 1;
	}

	public static Fraction Multiply(Fraction a, Fraction b, bool reduce = true) {
		// DO MAGIC FOR x/1000:
		if(a.denominator == 1000) {
			return new Fraction((int)(a.numerator * b.GetAsFloat()), 1000);
		}
		else if (b.denominator == 1000) {
			return new Fraction((int)(a.GetAsFloat() * b.numerator), 1000);
		}
		
		if (NumberTooBig(a,b)) return b;
		if(reduce){
//			Debug.Log("a, b:"+a+","+b);
			Fraction result = Fraction.ReduceFully(new Fraction(a.numerator * b.numerator, a.denominator * b.denominator));
			return result;
		}
		else {
			return Fraction.ReduceOverIntegers(new Fraction(a.numerator * b.numerator, a.denominator * b.denominator));
		}
	}

	public static bool Greater(Fraction a, Fraction b) {
		return (float)a.numerator / (float)a.denominator > (float)b.numerator / (float)b.denominator;
	}

	public static bool AbsGreater(Fraction a, Fraction b) {
		return Mathf.Abs((float)a.numerator / (float)a.denominator) > Mathf.Abs((float)b.numerator / (float)b.denominator);
	}

	public static Fraction Inverse(Fraction a){
		if (a.numerator < 0){
			return new Fraction(-a.denominator,-a.numerator);	
		} else {
	 		return new Fraction(a.denominator,a.numerator);
		}
	}

	public static Fraction Add(Fraction a, Fraction b, bool reduce = true) {
		// DO MAGIC FOR x/1000:
		if(a.denominator == 1000 && b.denominator == 1000) {
			return new Fraction(a.numerator + b.numerator, 1000);	
		}		
		if (NumberTooBig(a,b)) return b;
		
		// We usually learn to take the least common multiple of the two denoms, but
		// it doesn't really matter, we can use any common multiple since we're simplifying afterwards
		// anyways.
//		// commented Debug.Log("a and b: "+a+","+b);
		// This returns null ref exception somethetimes because "b" is not passed in (is null)

		if (a.denominator == 1 && b.denominator == 1){
			return new Fraction(a.numerator+b.numerator,1);
		}

		Fraction expA = new Fraction(a.numerator * b.denominator, a.denominator * b.denominator);
		Fraction expB = new Fraction(b.numerator * a.denominator, b.denominator * a.denominator);
		if(reduce) {
			return Fraction.ReduceFully(new Fraction(expA.numerator + expB.numerator, expA.denominator));
		}
		else {
			return Fraction.ReduceOverIntegers(new Fraction(expA.numerator + expB.numerator, expA.denominator));
		}
	}
	
	
	// TODO: find common de
	public static Fraction Subtract(Fraction a, Fraction b, bool reduce=true){
		// DO MAGIC FOR x/1000:
		if(a.denominator == 1000 && b.denominator == 1000) {
			return new Fraction(a.numerator - b.numerator, 1000);	
		}
		
		if (NumberTooBig(a,b)) return b; // why b?
		
		// answer: B - cause.
		Fraction expA = new Fraction(a.numerator * b.denominator, a.denominator * b.denominator);
		Fraction expB = new Fraction(b.numerator * a.denominator, b.denominator * a.denominator);

//		// commented Debug.Log ("expA B:"+expA+","+expB);

		if(reduce) {
			return Fraction.ReduceFully(new Fraction(expA.numerator - expB.numerator, expA.denominator));
		}
		else {
			return Fraction.ReduceOverIntegers(new Fraction(expA.numerator - expB.numerator, expA.denominator));
		}		
		
		
		
		
	}

	static bool NumberTooBig(Fraction a, Fraction b){
//		if (a.numerator + b.numerator + a.denominator + b.denominator > 4294967296){
//			// commented Debug.Log("number too big, exiting");
//			return true;
//		} else return false;
		return false;
	}
	


//	public static Fraction Divide(Fraction a, Fraction b, bool reduce=true){
//		Fraction expA = new Fraction(a.numerator * b.denominator, a.denominator * b.numerator);
//		if(reduce) {
//			return Fraction.ReduceFully(new Fraction(expA.numerator,expA.denominator));
//		}
//		else {
//			return Fraction.ReduceOverIntegers(new Fraction(expA.numerator, expA.denominator));
//		}
//	}
	
	
	public static Dictionary<Fraction, Fraction> memoReduceFully = new Dictionary<Fraction, Fraction>();
	// Will reduce to the simplest fraction
	public static Fraction ReduceFully(Fraction frac) {
		// Given 2048/8388608, fails
//		// commented Debug.Log ("Reducing fully:" + frac);

		if(memoReduceFully.ContainsKey(frac)) {
//			Debug.Log ("memo'd:"+memoReduceFully[frac]);
			return memoReduceFully[frac];
		}
//		int sign = (frac.numerator * frac.denominator > 0) ? 1 : -1;
		int sign = (frac.numerator * frac.denominator) < 0 ? -1 : 1;
//		if (frac.numerator < 0) sign = -1;
//		// commented Debug.Log("sign: "+ sign);
		int gcd = 1;
		int div;
		int min =Mathf.Min(Mathf.Abs(frac.numerator), Mathf.Abs(frac.denominator));
//		// commented Debug.Log("min:" +min+" from:"+frac);
		for (div=1;div<=min;div++){
			if (frac.numerator%div==0 && frac.denominator%div==0){
//				// commented Debug.Log ("gcd = div = "+gcd);
				gcd=div;
			}
		}
		Fraction newFrac = new Fraction(sign * Mathf.Abs(frac.numerator) / gcd, Mathf.Abs(frac.denominator) / gcd);
//		Debug.Log ("new frac:" + newFrac);
		memoReduceFully[frac] = newFrac;
		return newFrac;
	}

	public static Fraction GetAbsoluteValue(Fraction frac){
		
		return new Fraction(Mathf.Abs(frac.numerator),Mathf.Abs(frac.denominator));
	}

	// Will reduce to the simplest integer, if possible
	public static Fraction ReduceOverIntegers(Fraction frac) {
		if (frac.numerator % frac.denominator == 0) {
			return new Fraction(frac.numerator / frac.denominator, 1);
		}
		return frac;
	}
	
	public static Dictionary<Fraction, List<Fraction>> memoGetFactors = new Dictionary<Fraction, List<Fraction>>();
	public List<Fraction> GetFactors() {
		List<Fraction> ret = new List<Fraction>();
		int max = (int)Mathf.Sqrt(this.numerator);  //round down
		for(int factor = 1; factor <= max;factor++) { //test from 1 to the square root, or the int below it, inclusive.
			if(this.numerator % factor == 0) {
				ret.Add(new Fraction(factor,1));
				if(factor != this.numerator/factor) { // Don't add the square root twice!  Thanks Jon
					ret.Add(new Fraction(this.numerator/factor,1));
				}
			}
		}
		return ret;
	}


	
	
	public override string ToString ()
	{
//		return "";
		if(denominator == 1) {
			return numerator.ToString();	
		}
//		// commented Debug.Log ("checking mathf abs of fraction:" + numerator +"/"+denominator);
//		// commented Debug.Log (Mathf.Abs(numerator));
//		// commented Debug.Log (Mathf.Abs(denominator));
		if(Mathf.Abs(numerator) > Mathf.Abs(denominator)) { 
			int num = Mathf.Abs(numerator);
			int den = Mathf.Abs(denominator);
			int sign = (int)Mathf.Sign(numerator / denominator);
			int whole = (int)(num / den);
			int partsNum = num - (whole * den);
			return (sign * whole) + " " + partsNum + "/" + denominator;
		}
		else {
			return numerator + "/" + denominator;
		}
	}




	public bool IsTauFraction(Fraction frac)
	{
		float num = frac.GetAsFloat();
		float factorOf = (Mathf.PI / 8);
		
		int times = (int)Mathf.Round((float)num / factorOf);
		float off = Mathf.Abs(num - factorOf * times);
		if(off < 0.01f)
		{
			return true;
		}		
		return false;
	}
	
	public Fraction GetTauFraction(Fraction frac) 
	{
		float num = frac.GetAsFloat() / (Mathf.PI * 2);
		num *= 16;
		return Fraction.ReduceFully(new Fraction(Mathf.RoundToInt(num), 16));
	}
	
	
	// Smart math stuff. DO NOT TOUCH.
	// todo: Move this to MathUtils.cs
	string ReduceFraction(string fraction) {
		
		int Div=1;
		string[] frac = fraction.Split("/"[0]);
		int frac0=int.Parse(frac[0]);
		int frac1=int.Parse(frac[1]);
		for (var zxc0=1;zxc0<100;zxc0++){
			if (frac0%zxc0==0&&frac1%zxc0==0){
				Div=zxc0;
			}
		}
		var fractionString = (frac0/Div).ToString()+"/"+(frac1/Div).ToString();
		//// commented Debug.Log("Reduced "+fraction+" :"+fractionString);
		return fractionString;
	}
	
	// Smart math stuff. DO NOT TOUCH
	// approximate fractions from decimalNumbers here: view-source:http://www.mindspring.com/~alanh/fracs.html
	Fraction approximateFractions ( float decimalNumber  ){
		if (decimalNumber%1==0) {
			return new Fraction(decimalNumber,1);
		}
		float d= decimalNumber;
		int numerator=0;
		int denominator=0;
		
		float[] numerators = new float[100];
		numerators[0]=0;
		numerators[1]=1;
		float[] denominators = new float[100];
		denominators[0]=1;
		denominators[1]=0;
		
		int maxNumerator= getMaxNumerator(d);
		float d2= d;
		float calcD;
		float prevCalcD=.11f;
		
		//for (FIXME_VAR_TYPE i= 2; i < 1000; i++)  {
		
		for (var i= 2; i < 100; i++)  {
			var L2= Mathf.Floor(d2);
			numerators[i] = L2 * numerators[i-1] + numerators[i-2];
			if (Mathf.Abs(numerators[i]) > maxNumerator) {	
				// commented Debug.Log("oops: exceeded max numerator"); // breaks on -1/2
				return new Fraction(1,1);
			}
			
			denominators[i] = L2 * denominators[i-1] + denominators[i-2];
			// // commented Debug.Log("numerators["+i+"] set to "+numerators[i]+"/"+denominators[i]);
			
			calcD = numerators[i] / denominators[i];
			// // commented Debug.Log(numerators[i]+"/"+denominators[i]+", calcd:"+calcD+";prev:"+prevCalcD);
			// appendFractionsOutput(numerators[i], denominators[i]);
			//	    // commented Debug.Log("max numerator: "+maxNumerator+"; i:"+i);
			//		// commented Debug.Log("approx fraction of "+decimalNumber+" is "+numerators[i]+"/"+ denominators[i]);
			if (calcD == prevCalcD) {
				
				string numString="";
				string denString="";
				for (var j=0;j<i;j++){
					numString+=numerators[j]+",";
					denString+=denominators[j]+",";
				}
				// commented Debug.Log("oops--no incr. diff.. nums: "+numString+"_____denoms:"+denString);
				return new Fraction(-11,1);
			}
			
			if (calcD == d) {
				// // commented Debug.Log("Sweet. This fraction is exactly the same as the decimalNumber you requested. (within 3 digits)");
				numerator=(int)numerators[i];
				denominator=(int)denominators[i];
				return new Fraction(numerator,denominator);
			}
			
			prevCalcD = calcD;
			
			d2 = 1/(d2-L2);
		}
		
//		// commented Debug.Log("returning :"+numerator+"/"+denominator);
		
		return new Fraction(numerator,denominator);
		// }
		
	}
	
	// Smart math stuff. DO NOT TOUCH
	// DO NOT PASS INTEGERS INTO THIS FUNCTION. EACH TIME YOU DO, AN ELF RABBIT DIES. ALSO YOU WILL GET BS RESULTS.
	int getMaxNumerator ( float f  ){ 
		// clever genius stuff 
		
		int numDigits = f.ToString().Length-1;
		var numIntDigits= Mathf.Floor(f).ToString().Length;
		if (Mathf.Floor(f)==0) numIntDigits=0; 
		var numDigitsPastdecimalNumber= numDigits - numIntDigits;
		
		//	// commented Debug.Log("for f="+f+";int:"+numIntDigits+";float:"+numDigitsPastdecimalNumber);
		// Get the list of all integers in the 12345.678f by removing the decimalNumber
		var digits= int.Parse(f.ToString().Replace(".",""));
		//// commented Debug.Log("digits:"+digits);
		int L=digits;
		
		
		for (var i=numDigitsPastdecimalNumber; i>0 && L%2==0; i--) L/=2; // so clever
		for (var i=numDigitsPastdecimalNumber; i>0 && L%5==0; i--) L/=5;
		
		return L;
	}

	public static Fraction Divide(Fraction a, Fraction b){
		return Fraction.Multiply(a,Fraction.Inverse(b));
	}

	public static bool AIsFactorOfB(Fraction a, Fraction b){
//		// commented Debug.Log ("is "+a+" a factor of "+b+"?");
		if (Fraction.Divide(b,a).denominator == 1) {
//			// commented Debug.Log ("yes");
			return true;
		}
//		// commented Debug.Log ("no, frac div a,b:"+Fraction.Divide(a,b));
		return false;
	}
}

