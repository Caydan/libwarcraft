//
//  ModelTerrainCuttingPlanes.cs
//
//  Copyright (c) 2018 Jarl Gullberg
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
//

using System.Collections.Generic;
using System.IO;
using System.Numerics;
using Warcraft.Core.Extensions;
using Warcraft.Core.Interfaces;

namespace Warcraft.WMO.GroupFile.Chunks
{
    public class ModelTerrainCuttingPlanes : IIFFChunk, IBinarySerializable
    {
        /// <summary>
        /// Holds the binary chunk signature.
        /// </summary>
        public const string Signature = "MOPL";

        public List<Plane> CuttingPlanes = new List<Plane>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ModelTerrainCuttingPlanes"/> class.
        /// </summary>
        public ModelTerrainCuttingPlanes()
        {
        }

        public ModelTerrainCuttingPlanes(byte[] inData)
        {
            LoadBinaryData(inData);
        }

        /// <inheritdoc/>
        public void LoadBinaryData(byte[] inData)
        {
            using (MemoryStream ms = new MemoryStream(inData))
            {
                using (BinaryReader br = new BinaryReader(ms))
                {
                    while (ms.Position < ms.Length)
                    {
                        CuttingPlanes.Add(br.ReadPlane());
                    }
                }
            }
        }

        /// <inheritdoc/>
        public string GetSignature()
        {
            return Signature;
        }

        /// <inheritdoc/>
        public byte[] Serialize()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (BinaryWriter bw = new BinaryWriter(ms))
                {
                    foreach (Plane cuttingPlane in CuttingPlanes)
                    {
                        bw.WritePlane(cuttingPlane);
                    }
                }

                return ms.ToArray();
            }
        }
    }
}

