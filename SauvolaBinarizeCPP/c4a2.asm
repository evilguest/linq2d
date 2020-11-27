[SEGMENT .text]
global c4_avx2
c4_avx2:
	;c4_asm2 (&input[ w ],w,&output[ w ],h-2);
	;               rcx   rdx  r8        r9
	push rsi
	push rdi
	mov rdi,r8
	mov rsi,rcx
	sub rsi, rdx
	align (16)
@l2:
	mov r10,rdx
	align (16)	
@l1:
	
	vpmovzxbw ymm1,  [rcx-1]
	vpmovzxbw ymm2,  [rcx+1]
	vpaddw ymm3,ymm2,ymm1
	vpmovzxbw ymm4,[rcx+rdx]	
	vpmovzxbw ymm5, [rsi]
	vpaddw ymm3,ymm3,ymm4
	vpaddw ymm5,ymm3,ymm5
	vpsrlw ymm3,ymm5,2
	vpmovzxwd ymm1,xmm3
	vperm2i128 ymm2,ymm3,ymm3,1
	vpmovzxwd ymm3,xmm2	
	PREFETCHWT1 [rdi]
	vmovaps [rdi],ymm1
	vmovaps [rdi+32],ymm3
	add rcx,16
	add rdi,64
	add rsi,16
	
	sub r10,16
	jnz @l1

	dec r9
	jnz @l2	
	pop rdi
	pop rsi
	ret