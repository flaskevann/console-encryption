# console-encryption
Easy to use AES and RSA encryption accessible from the command console. Only needs .NET or [Mono](http://www.mono-project.com/) to work.

Each printed result is easy to parse. This makes <code>console-encryption</code> available for a lot of different purposes.

Every printed non-plain text is base64 encoded for easy storage and network transfers.

## AES

##### Generate new key and IV:

<pre>
Input:
> ce.exe AES 128

Output:
1DyHUn+9nFgkrt8HPwMIEA==
5lmuA0+2d8cWrr31wxWsfg==
</pre>

First line: The encryption and decryption key. Second line: Fresh new [IV](http://en.wikipedia.org/wiki/Initialization_vector).

##### Encrypt:

<pre>
Input:
> ce.exe AES encrypt "test" key iv

Output:
NSC1mj/lSmioBtAaBsJJdA==
</pre>

##### Decrypt:

<pre>
Input:
> ce.exe AES decrypt cipher key iv

Output:
"test"
</pre>

### RSA

##### Generate a set of keys:

<pre>
Input:
> ce.exe RSA 1024

Output:
PABSAFMAQQBLAGUAeQBWAGEAbAB1AGUAPgA8AE0AbwBkAHUAbAB1AHMAPgBqAEgAWAA5AHgATgBNAEkANwBwADUAQgBLAHIAVgBZAHIAYgB5AFgAMwA5AE4AcgBCAEsANgBKADMAVwBuAEYAcwBIAFkAeQBUAHAAUgA5ADMANgAyAEwAZgBIAHUAeQBUAHQAbgBMADIANgBHAHYAYQBwAEQAegBNAG0AZAA0ADEAYQBiAFgAbgBiAEIAZwB0ADMAKwBNAHMATgArAGUAOABKACsATgA3AGwAMgB6AHgAWgBDAGYAQwAxAEIAbAB3AGcAUgB5AEcASAA4AG4AcgBCAHAAaABLAFgAYwA3AEoASgArAEcAVgB1AFYATgBOAG8AQgBiAEMAQgA4AHIANQBuAEoAbgBhAFUAcQBPAC8AMQBKAHgAVABCADkANAA2AHQAeABPACsAawBDAHEARwBsAEMAawBZAHgAWQBvAEUAcQAyAGUAYgAzADIAMAA5AHAATQA9ADwALwBNAG8AZAB1AGwAdQBzAD4APABFAHgAcABvAG4AZQBuAHQAPgBBAFEAQQBCADwALwBFAHgAcABvAG4AZQBuAHQAPgA8AC8AUgBTAEEASwBlAHkAVgBhAGwAdQBlAD4A
PABSAFMAQQBLAGUAeQBWAGEAbAB1AGUAPgA8AE0AbwBkAHUAbAB1AHMAPgBqAEgAWAA5AHgATgBNAEkANwBwADUAQgBLAHIAVgBZAHIAYgB5AFgAMwA5AE4AcgBCAEsANgBKADMAVwBuAEYAcwBIAFkAeQBUAHAAUgA5ADMANgAyAEwAZgBIAHUAeQBUAHQAbgBMADIANgBHAHYAYQBwAEQAegBNAG0AZAA0ADEAYQBiAFgAbgBiAEIAZwB0ADMAKwBNAHMATgArAGUAOABKACsATgA3AGwAMgB6AHgAWgBDAGYAQwAxAEIAbAB3AGcAUgB5AEcASAA4AG4AcgBCAHAAaABLAFgAYwA3AEoASgArAEcAVgB1AFYATgBOAG8AQgBiAEMAQgA4AHIANQBuAEoAbgBhAFUAcQBPAC8AMQBKAHgAVABCADkANAA2AHQAeABPACsAawBDAHEARwBsAEMAawBZAHgAWQBvAEUAcQAyAGUAYgAzADIAMAA5AHAATQA9ADwALwBNAG8AZAB1AGwAdQBzAD4APABFAHgAcABvAG4AZQBuAHQAPgBBAFEAQQBCADwALwBFAHgAcABvAG4AZQBuAHQAPgA8AFAAPgB3AGMAdgBaAEEAVgBMAGsAZgBBAFMAagBmAGkAbgBCAHcATwBVAEEAKwBtAGoAKwBwAE8AcgBQAFUAcQA2AGIATQBZADQAOQB6ADcATABHAE8AUgA4AEMAaQA0AEgASQBWAGkAQwByAFcAbAB4AHgAeABrAG0AawBQAGwAbgBwAEoANgB6AG8ATAA1AG0ATwBDADgAbABPAG4AegBQADEAdgB1AHoAVwBtAHcAPQA9ADwALwBQAD4APABRAD4AdQBZAHUAWgBsAFkAdwBlAHMAUgBkAFcAbQBEADUANABrAGwAYQBDAEIAbQA2AHcALwBzAHkAawBJAGoATwBjAEIAcQBFAGcAVAA4ADkAWABXAEYAaQBqAG4AbABFAC8ARABJAGoAMgA2AEcAagBXAGQAcgBnAE4AUwBaADcAOAB5AGwAbwArAEIANgA3AHYAeAAvAEQASQAwAHEATwBmADEAbQBsAGoAYQBRAD0APQA8AC8AUQA+ADwARABQAD4ATwBoAEMATgBzAEcAdgBaAHMAWgA3AGkATABZAGIAdABXAHAASgBDAHYATQBTAEEAcAA0AEUARABKAFUANwBUAGkAUQBHAEoAVwA1AFgAQQA5AHAAUgBuACsAcwB5AGoANwBjAHAAVQBEAFAAcgB0AHkAKwA5ADgAbQBvAEgAdAA3AGMAbABNAHUAQgBaAFcARABRAG4ALwAyAEkAdQBFADgAZgBPAGMAUwBRAD0APQA8AC8ARABQAD4APABEAFEAPgBjAFoAQQB3AEEASQBnAHkAZwBOAEYAMwAvAHQAYQA5AGwAVgBPADkAWQBaAEoAKwBZAG4AVgArAFEARABDADIAWABwAGgAZgAzAG4AbABaAFQASQBsADAARwA0AEkAKwB4AEwAMQBQADQAZwAvAEkAdABBAEUAUwBvAE8AegBlAGIARAB1AHYAbQA2AFIARwB2AG0AbAB4AGcAUABzAEUANgBZAGEAMwBnAFEAPQA9ADwALwBEAFEAPgA8AEkAbgB2AGUAcgBzAGUAUQA+AFYAVABBADcAVABlAEQAOABkACsANABlAHkAeQBpAHoAVABrAGgASwBlAEYANABVAGMANQA4ADUASgBpAEkAZABDAGYAeABEAHgATgBaAGEAaABRAE4AVQBpAGMAcgBuAGIAcQB6AFMAbwBCAGkAQQBMAGgASwBvAEEAbQBnAHQAZQBCADAAbAB3AFYATgBXAEsARwBWAEYAMABYAHAAdABMAE0AQQAyAEMAZwA9AD0APAAvAEkAbgB2AGUAcgBzAGUAUQA+ADwARAA+AGEAeABsAHcAdABRAHAAQQB6AGsAbABHADQAVAB6AEQAYwBEADMAVQBBACsAdQBkAHQAMABVAHEAZwAvAFEAcABzADYASgA4AE8AMgBTAEkARQBlAHkAdQA1AGUAMABmAGkAWABZAFMATgBkAGEAegAvAFcAcQBtAHQAVABFAG0AZABxAG8AdgB6ACsASAB6ADcAUgBiAEoAZgBQAGcAVgBwAGQAMwBUAEIAdQBPAFcAaQBBAEkAOQBCAFQAZAA3AEEANABJAGkAYgBoAEIASgBrAHgARQBRAHYAUgA2AGwAVgBJAHAAeAArAEcARQAwADEANAA4AEkAOQB0AGUAegBzAEMAeABuAFQANwB2AFYATABMADkATgBaAHUAegBnAEEAWQB1AG4AMAB2AFkAZQAxAEUAYwBkAHgAZABLAEEATAA4AGQAbABtAFQAUABRAFEATABFAD0APAAvAEQAPgA8AC8AUgBTAEEASwBlAHkAVgBhAGwAdQBlAD4A
</pre>

First line: The public key, used for encryption. Second line: The private key, used for decryption.

##### Encrypt:

<pre>
Input:
> ce.exe RSA encrypt "test" public_key

Output:
S+Lk1x3E7vMKwlGPeoO9X+BjfkTIdq2TvYLuW/5XvwLKXft26GqVyU59DIq46+eQ79SY42Z7ETc5HPN4g8tNW0Yfd4dRslvSZoQzDYzPqmBUEh2pAIHD30XUFUCHCmVxrYrEmVjdJLifdXFTHYVBwJWMoSiYT8jkFFxdEOS+8cE=
</pre>

##### Decrypt:

<pre>
Input:
> ce.exe RSA decrypt cipher private_key

Output:
"test"
</pre>

### Tests

Run AES and RSA tests directly from console if needed:

<pre>
Input:
> ce.exe test

Output:
3x AES encryption and decryption tests>
2x RSA encryption and decryption tests>
</pre>

### Help

Run <code>> Encryption.exe</code> with no input:

<pre>
Console Encryption generates keys, encrypts and decrypts text.

AES uses:
==============================================================================
'ce.exe AES 128' generates a 128 key and 128 IV for you.
'ce.exe AES 192' generates a 192 key and 128 IV for you.
'ce.exe AES 256' generates a 256 key and 128 IV for you.
'ce.exe AES encrypt "your text" key iv' encrypts text for you.
'ce.exe AES decrypt cipher key iv' decrypts cipher for you.

Some RSA uses:
==============================================================================
'ce.exe RSA 1024' generates a 1024 public key and private key for you.
'ce.exe RSA 2048' generates a 2048 public key and private key for you.
'ce.exe RSA encrypt "your text" public_key' encrypts text for you.
'ce.exe RSA decrypt cipher private_key' decrypts cipher for you.

Skeptic?
==============================================================================
'ce.exe test' runs tests of all uses listed above!
</pre>

### Other uses

Import the encryption class into your own project and use it directly.
