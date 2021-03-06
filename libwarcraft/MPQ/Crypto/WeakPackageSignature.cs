﻿//
//  WeakPackageSignature.cs
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

using System.IO;

namespace Warcraft.MPQ.Crypto
{
    /// <summary>
    /// Represents a weak archive signature.
    /// </summary>
    public class WeakPackageSignature
    {
        /// <summary>
        /// Holds the internal filename of the signature file.
        /// </summary>
        public const string InternalFilename = "(signature)";

        /// <summary>
        /// Gets the package signature.
        /// </summary>
        public byte[] PackageSignature { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WeakPackageSignature"/> class.
        /// </summary>
        /// <param name="data">The binary data.</param>
        public WeakPackageSignature(byte[] data)
        {
            if (data.Length != 72)
            {
                throw new InvalidDataException("The provided data had an invalid length.");
            }

            using (MemoryStream ms = new MemoryStream(data))
            {
                using (BinaryReader br = new BinaryReader(ms))
                {
                    long identifier = br.ReadInt64();

                    if (identifier != 0)
                    {
                        throw new InvalidDataException("The signature did not begin with 0.");
                    }

                    PackageSignature = br.ReadBytes(64);
                }
            }
        }
    }
}
