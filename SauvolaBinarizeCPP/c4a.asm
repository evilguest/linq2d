_TEXT2 	SEGMENT EXECUTE align (16)
c4_asm1 proc
	; extern "C" void c4_asm1 (unsigned char * input,int width,int *output);
	; input aligned to 8, output to 16
	push rsi
	push rbx
	mov rsi,rcx
	sub rsi, rdx
	align (16)
l2:
	
	mov r11,rcx
	xor rbx,rbx
	mov r10,rdx
	pmovzxbw  xmm7,[rcx]
	vpslldq xmm6,xmm7,14
	mov rax,8
	psrldq xmm6,14
	align (16)

l1:	
	movdqa xmm0,xmm7
	pmovzxbw  xmm7,[rcx+rax]
	vpsrldq xmm1,xmm0,2
	vpslldq xmm2,xmm0,2
	vpslldq xmm8,xmm7,14
	pmovzxbw xmm3,[rsi]
	pmovzxbw xmm4,[rsi+rdx*2]	
	paddw xmm1,xmm2
	add rcx,rax
	paddw xmm3,xmm4
	paddw xmm6,xmm8
	add rsi,rax
	paddw xmm1,xmm3	
	paddw xmm1,xmm6
	psrlw xmm1,2
	vpsrldq xmm6,xmm0,14
	pmovzxwd xmm2,xmm1
	movdqa [r8],xmm2
	psrldq xmm1,8
	pmovzxwd xmm3,xmm1
	movdqa [r8+rax*2],xmm3
	lea r8,[r8+rax*4]
	sub r10,rax
	jnz l1
	dec r11
	movzx ax, byte ptr [r11+rdx]
	movzx bx, byte ptr [r11]
	add ax, bx
	movzx bx, byte ptr [r11+rdx-1]
	add ax, bx
	movzx bx, byte ptr [r11+2*rdx]
	add ax, bx
	shr ax,2
	mov word ptr [r8-4],ax
	dec r9
	jnz l2	
	pop rbx
	pop rsi
	ret
c4_asm1 endp

end