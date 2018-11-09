#
#
#

import sys
import copy

class State:
	move = [[0, -1, 0, -2], [0, 1, 0, 2], [-1, 0, -2, 0], [1, 0, 2, 0]]		# [上[杭x,y,穴x,y ] 下[...] 左[...] 右[...]]
	step = 0
	count = 0
	answer = []
	board = []
	peg   = []
	nextX = 0
	nextY = 0

	def __init__(self):
		self.board = [["x","x","o","o","o","x","x"],
					  ["x","x","o","o","o","x","x"],
					  ["o","o","o","o","o","o","o"],
					  ["o","o","o","_","o","o","o"],
					  ["o","o","o","o","o","o","o"],
					  ["x","x","o","o","o","x","x"],
					  ["x","x","o","o","o","x","x"]]
		for y in (range(len(self.board))):
			for x in (range(len(self.board[0]))):
				if (self.board[y][x] == "o"):
					self.peg.append([x, y])
					self.answer.append([None, None, None])

	def printBoard(self):
		for y in (range(len(self.board))):
			for x in (range(len(self.board[0]))):
				print str(self.board[y][x]) + ","
			print ""
		print str(len(self.peg)) + ":" + str(self.peg)
		print ""

	def checkComplete(self):
		return len(self.peg) == 1

	# 移動先確認
	def checkMove(self, direction, x, y):
		px = x + self.move[direction][0]
		py = y + self.move[direction][1]
		if (px < 0 or len(self.board[0]) <= px or py < 0 or len(self.board) <= py):
			return (0)
		hx = x + self.move[direction][2]
		hy = y + self.move[direction][3]
		if (hx < 0 or len(self.board[0]) <= hx or hy < 0 or len(self.board) <= hy):
			return (0)
		nextX = hx
		nextY = hy
		return (self.board[py][px] == "o" and self.board[hy][hx] == "_")

	# 移動進む
	def pegMove(self, direction, x, y):
		px = x + self.move[direction][0]
		py = y + self.move[direction][1]
		hx = x + self.move[direction][2]
		hy = y + self.move[direction][3]
		self.board[y][x] = "_"
		self.board[py][px] = "_"
		self.board[hy][hx] = "o"
		self.peg.remove([x,y])
		self.peg.remove([px,py])
		self.peg.append([hx,hy])
		self.count = self.count + 1

	# 移動戻る
	def pegUndo(self, direction, x, y):
		px = x + self.move[direction][0]
		py = y + self.move[direction][1]
		hx = x + self.move[direction][2]
		hy = y + self.move[direction][3]
		self.board[y][x] = "o"
		self.board[py][px] = "o"
		self.board[hy][hx] = "_"
		self.peg.append([x,y])
		self.peg.append([px,py])
		self.peg.remove([hx,hy])

d_str = ["U","D","L","R"]
FAIL = {}

def search(step, state):
	# 状態確認
	if (state.checkComplete()):
		# sys.stderr.write("完了")
		return (1)
	if (str(state.board) in FAIL):
		# sys.stderr.write("スキップ")
		return (0)
	# 次
	for p in (state.peg[:]):
		for d in (range(4)):
			if (state.checkMove(d, p[0], p[1])):
				# 進む
				state.answer[step] = [d, p[0], p[1]]
				state.pegMove(d, p[0], p[1])
				if (search(step + 1, state)):
					return (1)
				# 戻る
				state.pegUndo(d, p[0], p[1])
	# 手詰まり登録
	FAIL[str(state.board)] = 1
	return (0)

def main():
	# 問題
	state = State()
	print "問題"
	state.printBoard()

	# 探索開始
	if (not search(0, state)):
		# sys.stderr.write("無し")
		return

	# 結果
	print "解答"
	b = State()
	for a in state.answer:
		if (a[0] <> None):
			print str(d_str[a[0]]) + "(" + str(a[1]) + ", " + str(a[2]) +")"
			b.pegMove(a[0], a[1], a[2])
			b.printBoard()
	print "探索回数:" + str(state.count)
	for a in state.answer:
		if (a[0] <> None):
			print str(d_str[a[0]]) + " (" + str(a[1]) + ", " + str(a[2]) + ")"

main()
