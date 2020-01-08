﻿using System.Linq;

namespace System.Molecular.Core.@internal
{
    internal static class SD
    {

        public static readonly string[] AtomLabels =
        {
            "?",
            "H"  ,"He" ,"Li" ,"Be" ,"B"  ,"C"  ,"N"  ,"O"  ,
            "F"  ,"Ne" ,"Na" ,"Mg" ,"Al" ,"Si" ,"P"  ,"S"  ,
            "Cl" ,"Ar" ,"K"  ,"Ca" ,"Sc" ,"Ti" ,"V"  ,"Cr" ,
            "Mn" ,"Fe" ,"Co" ,"Ni" ,"Cu" ,"Zn" ,"Ga" ,"Ge" ,
            "As" ,"Se" ,"Br" ,"Kr" ,"Rb" ,"Sr" ,"Y"  ,"Zr" ,
            "Nb" ,"Mo" ,"Tc" ,"Ru" ,"Rh" ,"Pd" ,"Ag" ,"Cd" ,
            "In" ,"Sn" ,"Sb" ,"Te" ,"I"  ,"Xe" ,"Cs" ,"Ba" ,
            "La" ,"Ce" ,"Pr" ,"Nd" ,"Pm" ,"Sm" ,"Eu" ,"Gd" ,
            "Tb" ,"Dy" ,"Ho" ,"Er" ,"Tm" ,"Yb" ,"Lu" ,"Hf" ,
            "Ta" ,"W"  ,"Re" ,"Os" ,"Ir" ,"Pt" ,"Au" ,"Hg" ,
            "Tl" ,"Pb" ,"Bi" ,"Po" ,"At" ,"Rn" ,"Fr" ,"Ra" ,
            "Ac" ,"Th" ,"Pa" ,"U"  ,"Np" ,"Pu" ,"Am" ,"Cm" ,
            "Bk" ,"Cf" ,"Es" ,"Fm" ,"Md" ,"No" ,"Lr" ,"Rf" ,
            "Db" ,"Sg" ,"Bh" ,"Hs" ,"Mt" ,"Ds" ,"Rg" ,"Cn" ,
            "Nh" ,"Fl" ,"Mc" ,"Lv" ,"Ts" ,"Og" ,"??" ,"??" ,
            "??" ,"??" ,"??" ,"??" ,"??" ,"??" ,"??" ,"??" ,
            "R4" ,"R5" ,"R6" ,"R7" ,"R8" ,"R9" ,"R10","R11",
            "R12","R13","R14","R15","R16","R1" ,"R2" ,"R3" ,
            "A"  ,"A1" ,"A2" ,"A3" ,"??" ,"??" ,"D"  ,"T"  ,
            "X"  ,"R"  ,"H2" ,"H+" ,"Nnn","HYD","Pol","??" ,
            "??" ,"??" ,"??" ,"??" ,"??" ,"??" ,"??" ,"??" ,
            "??" ,"??" ,"Ala","Arg","Asn","Asp","Cys","Gln",
            "Glu","Gly","His","Ile","Leu","Lys","Met","Phe",
            "Pro","Ser","Thr","Trp","Tyr","Val"
        };
        public static readonly float[] RelativeMass = new [] { 
                 0.0,
                 1.00794, 4.0026, 6.9410, 9.0122, 10.811, 12.011,   //  H , He, Li, Be, B , C , 
	             14.007, 15.999, 18.998, 20.180, 22.990, 24.305,    //  N , O , F , Ne, Na, Mg, 
	             26.982, 28.086, 30.974, 32.066, 35.453, 39.948,    //  Al, Si, P , S , Cl, Ar, 
	             39.098, 40.078, 44.956, 47.867, 50.942, 51.996,    //  K , Ca, Sc, Ti, V , Cr, 
	             54.938, 55.845, 58.933, 58.693, 63.546, 65.390,    //  Mn, Fe, Co, Ni, Cu, Zn, 
	             69.723, 72.610, 74.922, 78.960, 79.904, 83.800,    //  Ga, Ge, As, Se, Br, Kr, 
	             85.468, 87.620, 88.906, 91.224, 92.906, 95.940,    //  Rb, Sr, Y , Zr, Nb, Mo, 
	             98.906, 101.07, 102.91, 106.42, 107.87, 112.41,    //  Tc, Ru, Rh, Pd, Ag, Cd, 
	             114.82, 118.71, 121.76, 127.60, 126.90, 131.29,    //  In, Sn, Sb, Te, I , Xe, 
	             132.91, 137.33, 138.91, 140.12, 140.91, 144.24,    //  Cs, Ba, La, Ce, Pr, Nd, 
	             146.92, 150.36, 151.96, 157.25, 158.93, 162.50,    //  Pm, Sm, Eu, Gd, Tb, Dy, 
	             164.93, 167.26, 168.93, 173.04, 174.97, 178.49,    //  Ho, Er, Tm, Yb, Lu, Hf, 
	             180.95, 183.84, 186.21, 190.23, 192.22, 195.08,    //  Ta, W,  Re, Os, Ir, Pt, 
	             196.97, 200.59, 204.38, 207.20, 208.98, 209.98,    //  Au, Hg, Tl, Pb, Bi, Po, 
	             209.99, 222.02, 223.02, 226.03, 227.03, 232.04,    //  At, Rn, Fr, Ra, Ac, Th, 
	             231.04, 238.03, 237.05, 239.05, 241.06, 244.06,    //  Pa, U,  Np, Pu, Am, Cm, 
	             249.08, 252.08, 252.08, 257.10, 258.10, 259.10,    //  Bk, C,  Es, Fm, Md, No, 
	             262.11, 267.12, 268.13, 271.13, 270.13, 277.15,    //  Lr, Rf, Db, Sg, Bh, Hs,
	             276.15, 281.17, 281.17, 283.17, 285.18, 289.19,    //  Mt ,Ds ,Rg ,Cn ,Nh ,Fl,
	             289.19, 293.20, 294.21, 294.21,    0.0,    0.0,    //  Mc ,Lv ,Ts ,Og ,?? ,??,
	                0.0,    0.0,    0.0,    0.0,    0.0,    0.0,    //  ??, ??, ??, ??, ??, ??, 
	                0.0,    0.0,    0.0,    0.0,    0.0,    0.0,    //  ??, ??, R4, R5, R6, R7, 
	                0.0,    0.0,    0.0,    0.0,    0.0,    0.0,    //  R8, R9, R10,R11,R12,R13, 
	                0.0,    0.0,    0.0,    0.0,    0.0,    0.0,    //  R14,R15,R16,R1, R2, R3, 
	                0.0,    0.0,    0.0,    0.0,    0.0,    0.0,    //  A , A1, A2, A3, ??, ??, 
	             2.0141, 3.0160,    0.0,    0.0,    0.0,    0.0,    //  D , T , X , R , H2, H+, 
	                0.0,    0.0,    0.0,    0.0,    0.0,    0.0,    //  Nnn,HYD,Pol,??, ??, ??, 
	                0.0,    0.0,    0.0,    0.0,    0.0,    0.0,    //  ??, ??, ??, ??, ??, ??, 
	                0.0,    0.0, 71.0787, 156.18828, 114.10364, 115.0877,  // ??, ??, Ala,Arg,Asn,Asp,    relative masses of amino acids reduced by one H2O
	            103.1447,  128.13052, 129.11458,  57.05182, 137.14158, 113.15934, //  Cys,Gln,Glu,Gly,His,Ile,
	            113.15934, 128.17428, 131.19846, 147.17646,  97.11658,  87.0777,  //  Leu,Lys,Met,Phe,Pro,Ser,
	            101.10458, 186.2134 , 163.17546,  99.13246          //  Thr,Trp,Tyr,Val,
        }.Select(x => (float) x).ToArray(); // todo

		public static readonly float[] AbsoluteMass = new[]
        { 
            0.0,
	       1.007825,   4.00260 ,   7.016003,   9.012182,	//  H, He,Li,Be
	      11.009305,  12.000000,  14.003074,  15.994915,	//  B ,C ,N, O
	      18.998403,  19.992435,  22.989767,  23.985042,	//  F ,Ne,Na,Mg
	      26.98153 ,  27.976927,  30.973762,  31.972070,	//  Al,Si,P ,S
	      34.968852,  39.962384,  38.963707,  39.962591,	//  Cl,Ar,K ,Ca
	      44.955910,  47.947947,  50.943962,  51.940509,	//  Sc,Ti,V ,Cr
	      54.938047,  55.934939,  58.933198,  57.935346,	//  Mn,Fe,Co,Ni
	      62.939598,  63.929145,  68.925580,  73.921177,	//  Cu,Zn,Ga,Ge
	      74.921594,  79.916520,  78.918336,  83.911507,	//  As,Se,Br,Kr
	      84.911794,  87.905619,  88.905849,  89.904703,	//  Rb,Sr,Y ,Zr
	      92.906377,  97.905406,  89.923810, 101.904348,	//  Nb,Mo,Tc,Ru
	     102.905500, 105.903478, 106.905092, 113.903357,	//  Rh,Pd,Ag,Cd
	     114.903880, 119.902200, 120.903821, 129.906229,	//  In,Sn,Sb,Te
	     126.904473, 131.904144, 132.905429, 137.905232,	//  I ,Xe,Cs,Ba
	     138.906346, 139.905433, 140.907647, 141.907719,	//  La,Ce,Pr,Nd
	     135.923980, 151.919729, 152.921225, 157.924099,	//  Pm,Sm,Eu,Gd
	     158.925342, 163.929171, 164.930319, 165.930290,	//  Tb,Dy,Ho,Er
	     168.934212, 173.938859, 174.940770, 179.946545,	//  Tm,Yb,Lu,Hf
	     180.947992, 183.950928, 186.955744, 191.961467,	//  Ta,W, Re,Os
	     192.962917, 194.964766, 196.966543, 201.970617,	//  Ir,Pt,Au,Hg
	     204.974401, 207.976627, 208.980374, 193.988180,	//  Tl,Pb,Bi,Po
	     195.995730, 199.995700, 201.004110, 206.003800,	//  At,Rn,Fr,Ra
	     210.009230, 232.038054, 216.018960, 238.050784,	//  Ac,Th,Pa,U
	     229.036230, 232.041169, 237.050050, 238.053020,	//  Np,Pu,Am,Cm
	     242.061940, 240.062280, 243.069470, 243.074460,	//  Bk,C ,Es,Fm
	     248.082750, 251.088870, 253.095150, 257.102950,	//  Md,No,Lr,Rf
	     257.107770, 271.13,     270.13,     277.15,		//  Db,Sg,Bh,Hs
	     276.15,     281.17,     281.17,     283.17,		//  Mt,Ds,Rg,Cn
	     285.18,     289.19,     289.19,     291.20,		//  Nh,Fl,Mc,Lv
	     294.21,     294.21,       0.0f,       0.0f,		//  Ts,Og,??,??
	       0.0f,       0.0f,       0.0f,       0.0f,		//  ??,??,??,??
	       0.0f,       0.0f,       0.0f,       0.0f,		//  ??,??,??,??
	       0.0f,       0.0f,       0.0f,       0.0f,		//  R4,R5,R6,R7
	       0.0f,       0.0f,       0.0f,       0.0f,		//  R8,R9,R10,R11
	       0.0f,       0.0f,       0.0f,       0.0f,		//  R12,R13,R14,R15
	       0.0f,       0.0f,       0.0f,       0.0f,		//  R16,R1,R2,R3
	       0.0f,       0.0f,       0.0f,       0.0f,		//  A ,A1,A2,A3
	       0.0f,       0.0f,       2.0140,     3.01605,		//  ??,??,D ,T
	       0.0f,       0.0f,       0.0f,       0.0f,   	    //  X ,R ,H2,H+
           0.0f,       0.0f,       0.0f,       0.0f,        //  Nnn,HYD,Pol,??
           0.0f,       0.0f,       0.0f,       0.0f,        //  ??,??,??,??
           0.0f,       0.0f,       0.0f,       0.0f,        //  ??,??,??,??
           0.0f,       0.0f,       0.0f,       0.0f,        //  ??, ??, Ala,Arg
           0.0f,       0.0f,       0.0f,       0.0f,        //  Asn,Asp,,Cys,Gln
           0.0f,       0.0f,       0.0f,       0.0f,        //  Glu,Gly,His,Ile,
           0.0f,       0.0f,       0.0f,       0.0f,        //  Leu,Lys,Met,Phe
           0.0f,       0.0f,       0.0f,       0.0f,        //  Pro,Ser,Thr,Trp,
           0.0f,       0.0f,                                //  Tyr,Val
	    }.Select(x => (float)x).ToArray(); // todo

	}
}