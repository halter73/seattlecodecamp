// LibGd.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "gd.h"

gdImagePtr loadPng(char *filename)
{
		FILE *in;
		struct stat stat_buf;
		gdImagePtr im;
		in = fopen(filename, "rb");
		if (!in) {
				printf("error0");
				/* Error */
		}
		if (fstat(_fileno(in), &stat_buf) != 0) {
				printf("error1");
				/* Error */
		}
		/* Read the entire thing into a buffer
		that we allocate */
		char *buffer = (char*)malloc(stat_buf.st_size);
		if (!buffer) {
				printf("error2");
				/* Error */
		}
		if (fread(buffer, 1, stat_buf.st_size, in)
				!= stat_buf.st_size)
		{
				printf("error3");
				/* Error */
		}
		im = gdImageCreateFromPngPtr(
				stat_buf.st_size, buffer);
		/* WE allocated the memory, WE free
		it with our normal free function */
		free(buffer);
		fclose(in);
		return im;
}

void savePng(char *filename, gdImagePtr im)
{
		FILE *out;
		int size;
		char *data;
		out = fopen(filename, "wb");
		if (!out) {
				printf("error4");
				/* Error */
		}
		data = (char *)gdImagePngPtr(im, &size);
		if (!data) {
				printf("error5");
				/* Error */
		}
		if (fwrite(data, 1, size, out) != size) {
				printf("error6");
				/* Error */
		}
		if (fclose(out) != 0) {
				printf("error7");
				/* Error */
		}
		gdFree(data);
}

gdImagePtr resizeImg(gdImagePtr im, int sx, int sy)
{
		gdImagePtr im_out;
		/* Make the output image four times as large on both axes */
		im_out = gdImageCreate(sx, sy);
		/* Now copy the smaller image, but four times larger */
		gdImageCopyResized(im_out, im, 0, 0, 0, 0,
				sx, sy,
				im->sx, im->sy);

		return im_out;
}

int main(int argc, char **argv)
{
		assert(argc == 3);

		gdImagePtr img = loadPng(argv[1]);
		gdImagePtr resized_img = resizeImg(img, 64, 64);
		savePng(argv[2], resized_img);

		return 0;
}

