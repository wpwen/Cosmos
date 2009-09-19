﻿using System;
using System.Linq;

namespace Cosmos.IL2CPU.X86 {
    /// <summary>
    /// Subtracts the source operand from the destination operand and 
    /// replaces the destination operand with the result. 
    /// </summary>
    [OpCode("sub")]
    public class Sub : InstructionWithDestinationAndSourceAndSize {
        public static void InitializeEncodingData(Instruction.InstructionData aData) {
            aData.EncodingOptions.Add(new InstructionData.InstructionEncodingOption {
                OpCode = new byte[] { 0x28},
                NeedsModRMByte=true,
                InitialModRMByteValue = 0xC0,
                OperandSizeByte = 0,
                AllowedSizes = InstructionSizes.Byte | InstructionSizes.Word | InstructionSizes.DWord,
                DefaultSize = InstructionSize.DWord,
                DestinationRegAny = true,
                SourceRegAny = true,
                ReverseRegisters=true
            }); // register to register
            aData.EncodingOptions.Add(new InstructionData.InstructionEncodingOption {
                OpCode = new byte[]{0x2C},
                OperandSizeByte = 0,
                AllowedSizes = InstructionSizes.Byte | InstructionSizes.Word | InstructionSizes.DWord,
                DefaultSize = InstructionSize.DWord,
                DestinationReg=Registers.EAX,
                SourceImmediate=true
            }); // immediate to register (EAX, AX, AL)
            aData.EncodingOptions.Add(new InstructionData.InstructionEncodingOption {
                OpCode = new byte[] { 0x80, 0xE8 },
                OperandSizeByte = 0,
                AllowedSizes = InstructionSizes.Byte | InstructionSizes.Word | InstructionSizes.DWord,
                DefaultSize = InstructionSize.DWord,
                DestinationRegAny = true,
                DestinationRegByte = 1,
                SourceImmediate = true
            }); // immediate to register
            aData.EncodingOptions.Add(new InstructionData.InstructionEncodingOption {
                OpCode = new byte[]{0x80},
                OperandSizeByte = 0,
                AllowedSizes = InstructionSizes.Byte | InstructionSizes.Word | InstructionSizes.DWord,
                DefaultSize = InstructionSize.DWord,
                NeedsModRMByte=true,
                InitialModRMByteValue = 0x28,
                SourceImmediate=true,
                DestinationMemory=true,
                ReverseRegisters=true
            }); // immediate to memory
            aData.EncodingOptions.Add(new InstructionData.InstructionEncodingOption {
                OpCode = new byte[] { 0x28 },
                OperandSizeByte = 0,
                AllowedSizes = InstructionSizes.Byte | InstructionSizes.Word | InstructionSizes.DWord,
                DefaultSize = InstructionSize.DWord,
                NeedsModRMByte = true,
                //InitialModRMByteValue = 0x28,
                SourceRegAny = true,
                DestinationMemory = true,
                ReverseRegisters=true
            }); // register to memory
            aData.EncodingOptions.Add(new InstructionData.InstructionEncodingOption {
                OpCode = new byte[] { 0x2A },
                OperandSizeByte = 0,
                AllowedSizes = InstructionSizes.Byte | InstructionSizes.Word | InstructionSizes.DWord,
                DefaultSize = InstructionSize.DWord,
                NeedsModRMByte = true,
                SourceMemory = true,
                DestinationRegAny = true
            }); // memory to register
        }
    }
}