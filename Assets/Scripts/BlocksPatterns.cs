using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocksPatterns {
    // cells of blocks
    public static int[] IBlock = { 1, 1, 2, 1, 3, 1, 4, 1 }; //
    public static int[] JBlock = { 1, 1, 1, 2, 2, 2, 3, 2 }; //
    public static int[] LBlock = { 3, 1, 1, 2, 2, 2, 3, 2 }; // 
    public static int[] OBlock = { 1, 1, 2, 1, 1, 2, 2, 2 }; // 
    public static int[] SBlock = { 2, 1, 3, 1, 1, 2, 2, 2 }; //
    public static int[] TBlock = { 2, 1, 1, 2, 2, 2, 3, 2 }; //
    public static int[] ZBlock = { 1, 1, 2, 1, 2, 2, 3, 2 }; //

    public static List<int[]> patterns = new List<int[]>{ IBlock, JBlock, LBlock, OBlock, SBlock, TBlock, ZBlock };
}
